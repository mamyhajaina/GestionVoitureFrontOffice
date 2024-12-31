using GestionVoitureFrontOffice.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace GestionVoitureFrontOffice.Services
{
    public class TragerService
    {
        public readonly string _connectionString;

        public TragerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<NosTrager>> getAllNosTragersync()
        {
            var data = new List<NosTrager>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [Id] ,[Destination]
                                FROM [NosTrager]";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var row = new NosTrager
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            Destination = reader["Destination"].ToString(),

                        };
                        data.Add(row);
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
            return data;
        }


        public async Task<List<NosTrager>> GetByIdVehicleDepartVehicleAsync(int idVehicle)
        {
            var data = new List<NosTrager>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Requête avec jointure entre TragerVehicule et NosTrager
                string query = @"
                        SELECT nt.[Id], nt.[Destination]
                        FROM [TragerVehicule] tv
                        INNER JOIN [NosTrager] nt ON tv.[idTragerDepart] = nt.[Id]
                        WHERE tv.[idVehicle] = @IdVehicle";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdVehicle", idVehicle); // Ajout du paramètre pour filtrer par véhicule

                    try
                    {
                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new NosTrager
                                {
                                    Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                    Destination = reader["Destination"]?.ToString(),
                                };
                                data.Add(row);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while fetching data: {ex.Message}");
                    }
                }
            }
            return data;
        }

        public async Task<List<NosTrager>> GetByIdVehicleArriverVehicleAsync(int idVehicle)
        {
            var data = new List<NosTrager>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Requête avec jointure entre TragerVehicule et NosTrager
                string query = @"
                        SELECT nt.[Id], nt.[Destination]
                        FROM [TragerVehicule] tv
                        INNER JOIN [NosTrager] nt ON tv.[idTragerArriver] = nt.[Id]
                        WHERE tv.[idVehicle] = @IdVehicle";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdVehicle", idVehicle); // Ajout du paramètre pour filtrer par véhicule

                    try
                    {
                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new NosTrager
                                {
                                    Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                    Destination = reader["Destination"]?.ToString(),
                                };
                                data.Add(row);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while fetching data: {ex.Message}");
                    }
                }
            }
            return data;
        }


        public async Task<decimal> searcheVehicleLocationWithTragerAsync(int idVehicle, int idTragerDepart, int idTragerArriver)
        {
            decimal reponseDefaultLocation = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Requête SQL pour rechercher le prix de location
                string query = @"
            SELECT tv.prixLocation
            FROM TragerVehicule tv
            WHERE tv.idVehicle = @idVehicle 
              AND tv.idTragerDepart = @idTragerDepart
              AND tv.idTragerArriver = @idTragerArriver";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Ajout des paramètres
                    cmd.Parameters.AddWithValue("@idVehicle", idVehicle);
                    cmd.Parameters.AddWithValue("@idTragerDepart", idTragerDepart);
                    cmd.Parameters.AddWithValue("@idTragerArriver", idTragerArriver);

                    try
                    {
                        await con.OpenAsync();

                        // Exécution de la requête
                        var result = await cmd.ExecuteScalarAsync();

                        // Vérification du résultat et conversion
                        if (result != null && result != DBNull.Value)
                        {
                            reponseDefaultLocation = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return reponseDefaultLocation;
        }

    }
}
