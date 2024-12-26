using GestionVoitureFrontOffice.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace GestionVoitureFrontOffice.Services
{
    public class NosTragerService
    {
        public readonly string _connectionString;

        public NosTragerService(IConfiguration configuration)
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
    }
}
