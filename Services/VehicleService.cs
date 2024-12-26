using GestionVoitureFrontOffice.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace GestionVoitureFrontOffice.Services
{
    public class VehicleService
    {
        public readonly string _connectionString;

        public VehicleService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Vehicle>> getAllVehcilesync()
        {
            var vehicles = new List<Vehicle>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT v.[Id] ,v.[Number] ,v.[Pseudo] ,v.[typeVehicleId] ,v.[Brand] ,v.[Model] ,v.[Capacity] ,v.[Descriptions] ,v.[TableKilometer] ,v.[PhotoUrl], tv.[Name] AS TypeVehicleName
                                FROM [Vehicles] v INNER JOIN [TypeVehicle] tv ON v.[typeVehicleId] = tv.[id]";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var vehicle = new Vehicle
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            Number = reader["Number"] != DBNull.Value ? reader["Number"].ToString() : string.Empty,
                            Pseudo = reader["Pseudo"] != DBNull.Value ? reader["Pseudo"].ToString() : string.Empty,
                            TypeVehicleId = reader["TypeVehicleId"] != DBNull.Value ? Convert.ToInt32(reader["TypeVehicleId"]) : 0,
                            Brand = reader["Brand"] != DBNull.Value ? reader["Brand"].ToString() : string.Empty,
                            Model = reader["Model"] != DBNull.Value ? reader["Model"].ToString() : string.Empty,
                            Capacity = reader["Capacity"] != DBNull.Value ? Convert.ToInt32(reader["Capacity"]) : 0,
                            Descriptions = reader["Descriptions"] != DBNull.Value ? reader["Descriptions"].ToString() : string.Empty,
                            TableKilometer = reader["TableKilometer"] != DBNull.Value ? Convert.ToInt32(reader["TableKilometer"]) : 0,
                            PhotoUrl = reader["PhotoUrl"] != DBNull.Value ? reader["PhotoUrl"].ToString() : string.Empty,
                            TypeVehicle = new TypeVehicle()
                            {
                                Id = reader["TypeVehicleId"] != DBNull.Value ? Convert.ToInt32(reader["TypeVehicleId"]) : 0,
                                Name = reader["TypeVehicleName"] != DBNull.Value ? reader["TypeVehicleName"].ToString() : string.Empty,
                            }
                        };
                        vehicles.Add(vehicle);
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
            return vehicles;
        }

        public async Task<Vehicle> getVehcileByIdAsync(int id)
        {
            Vehicle vehicle = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    SELECT v.[Id] ,v.[Number] ,v.[Pseudo] ,v.[typeVehicleId] ,v.[Brand] ,v.[Model] ,v.[Capacity] ,v.[Descriptions] ,v.[TableKilometer] ,v.[PhotoUrl], tv.[Name] AS TypeVehicleName
                            FROM [Vehicles] v INNER JOIN [TypeVehicle] tv ON v.[typeVehicleId] = tv.[id]
                    WHERE v.[Id] = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            vehicle = new Vehicle()
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                Number = reader["Number"] != DBNull.Value ? reader["Number"].ToString() : string.Empty,
                                Pseudo = reader["Pseudo"] != DBNull.Value ? reader["Pseudo"].ToString() : string.Empty,
                                TypeVehicleId = reader["TypeVehicleId"] != DBNull.Value ? Convert.ToInt32(reader["TypeVehicleId"]) : 0,
                                Brand = reader["Brand"] != DBNull.Value ? reader["Brand"].ToString() : string.Empty,
                                Model = reader["Model"] != DBNull.Value ? reader["Model"].ToString() : string.Empty,
                                Capacity = reader["Capacity"] != DBNull.Value ? Convert.ToInt32(reader["Capacity"]) : 0,
                                Descriptions = reader["Descriptions"] != DBNull.Value ? reader["Descriptions"].ToString() : string.Empty,
                                TableKilometer = reader["TableKilometer"] != DBNull.Value ? Convert.ToInt32(reader["TableKilometer"]) : 0,
                                PhotoUrl = reader["PhotoUrl"] != DBNull.Value ? reader["PhotoUrl"].ToString() : string.Empty,
                                TypeVehicle = new TypeVehicle()
                                {
                                    Id = reader["TypeVehicleId"] != DBNull.Value ? Convert.ToInt32(reader["TypeVehicleId"]) : 0,
                                    Name = reader["TypeVehicleName"] != DBNull.Value ? reader["TypeVehicleName"].ToString() : string.Empty,
                                }
                            };
                        }
                    }
                }
            }

            return vehicle;
        }
    }
}
