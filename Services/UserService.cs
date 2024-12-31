using GestionVoitureFrontOffice.Models;
using System.Data.SqlClient;

namespace GestionVoitureFrontOffice.Services
{
    public class UserService
    {
        public readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<User>> getAllUsersync()
        {
            var users = new List<User>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : null,
                            IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : null
                        };
                        users.Add(user);
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
            return users;
        }

        public async Task<User> LoginAsync(string email, string passwordHash)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    SELECT u.[Id], u.[FirstName], u.[LastName], u.[Email], u.[PasswordHash], u.[RoleId], u.[IsActive], u.[CreatedAt], u.[UpdatedAt],
                           r.[Id] AS RoleId, r.[Name] as [RoleName]
                    FROM [User] u
                    INNER JOIN [RoleUser] r ON u.[RoleId] = r.[Id]
                    WHERE u.[Email] = @Email AND u.[PasswordHash] = @PasswordHash AND u.[IsActive] = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                RoleId = reader["RoleId"] != DBNull.Value ? Convert.ToInt32(reader["RoleId"]) : 0,
                                IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : null,
                                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : null,
                                RoleUser = new RoleUser
                                {
                                    Id = Convert.ToInt32(reader["RoleId"]),
                                    Name = reader["RoleName"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            return user;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
            INSERT INTO [User] (FirstName, LastName, Email, PasswordHash, RoleId, CreatedAt, UpdatedAt, IsActive) 
            VALUES (@FirstName, @LastName, @Email, @PasswordHash, @RoleId, @CreatedAt, @UpdatedAt, @IsActive)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ajout des paramètres pour éviter les injections SQL
                        command.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RoleId", user.RoleId);
                        command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt ?? DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedAt", user.UpdatedAt ?? DateTime.Now);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive ?? true);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Retourne vrai si l'insertion a réussi
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log de l'exception (à remplacer par un logger dans un projet réel)
                Console.WriteLine("Erreur SQL : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Log d'autres types d'erreurs
                Console.WriteLine("Erreur : " + ex.Message);
                return false;
            }
        }


        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
            UPDATE User
            SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PasswordHash = @PasswordHash, 
                RoleId = @RoleId, UpdatedAt = @UpdatedAt, IsActive = @IsActive
            WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM User WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task<bool> CheckUserAsync(string email)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    SELECT u.[Id], u.[FirstName], u.[LastName], u.[Email], u.[PasswordHash], u.[RoleId], u.[IsActive], u.[CreatedAt], u.[UpdatedAt],
                           r.[Id] AS RoleId, r.[Name] as [RoleName]
                    FROM [User] u
                    INNER JOIN [RoleUser] r ON u.[RoleId] = r.[Id]
                    WHERE u.[Email] = @Email AND u.[IsActive] = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                RoleId = reader["RoleId"] != DBNull.Value ? Convert.ToInt32(reader["RoleId"]) : 0,
                                IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : null,
                                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : null,
                                RoleUser = new RoleUser
                                {
                                    Id = Convert.ToInt32(reader["RoleId"]),
                                    Name = reader["RoleName"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            return user == null;
        }

    }
}
