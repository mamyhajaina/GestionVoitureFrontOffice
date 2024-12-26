using GestionVoitureFrontOffice.Models;
using System.Data.SqlClient;

namespace GestionVoitureFrontOffice.Services
{
    public class OffreService
    {
        public readonly string _connectionString;

        public OffreService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Offer>> getAllOffreAsync()
        {
            var offers = new List<Offer>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP (1000) [Id],[idTragerDeparature] ,[idClient] ,[idVehicle] ,[otherTragerDescription] ,[description] ,[dateMission] ,[totalAmount] ,[created_at] ,[idTragerArriving] ,[capacity] FROM [Offer]";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var offer = new Offer
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdTragerDeparture = Convert.ToInt32(reader["idTragerDeparature"]),
                            IdTragerArriving = Convert.ToInt32(reader["idTragerArriving"]),
                            IdClient = Convert.ToInt32(reader["idClient"]),
                            IdVehicle = Convert.ToInt32(reader["idVehicle"]),
                            OtherTragerDescription = reader["otherTragerDescription"].ToString(),
                            Description = reader["description"].ToString(),
                            DateMission = Convert.ToDateTime(reader["dateMission"]),
                            TotalAmount = Convert.ToDecimal(reader["totalAmount"]),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            Capacity = Convert.ToInt32(reader["capacity"])
                        };
                        offers.Add(offer);
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
            return offers;
        }

        public async Task<bool> AddOffre(Offer data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                INSERT INTO [Offer] 
                (idTragerDeparature, idClient, idVehicle, otherTragerDescription, description, dateMission, totalAmount, created_at, idTragerArriving, capacity, NameSociete, Email) 
                VALUES 
                (@idTragerDeparature, @idClient, @idVehicle, @otherTragerDescription, @description, @dateMission, @totalAmount, @created_at, @idTragerArriving, @capacity, @NameSociete, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ajout des paramètres pour éviter les injections SQL
                        command.Parameters.AddWithValue("@idTragerDeparature", data.IdTragerDeparture ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idClient", data.IdClient ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idVehicle", data.IdVehicle ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@otherTragerDescription", string.IsNullOrEmpty(data.OtherTragerDescription) ? (object)DBNull.Value : data.OtherTragerDescription);
                        command.Parameters.AddWithValue("@description", string.IsNullOrEmpty(data.Description) ? (object)DBNull.Value : data.Description);
                        command.Parameters.AddWithValue("@dateMission", data.DateMission);
                        command.Parameters.AddWithValue("@totalAmount", data.TotalAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idTragerArriving", data.IdTragerArriving ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@capacity", data.Capacity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NameSociete", data.NameSociete ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", data.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@created_at", DateTime.Now);

                        connection.Open();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
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

    }
}
