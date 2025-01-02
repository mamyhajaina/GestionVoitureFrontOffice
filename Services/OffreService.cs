using GestionVoitureFrontOffice.Models;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                string query = @"
                            SELECT 
                                o.[Id], o.[idTragerDeparture], o.[idClient], o.[idVehicle], 
                                o.[otherTragerDescription], o.[description], o.[dateMission], 
                                o.[totalAmount], o.[created_at], o.[idTragerArriving], o.[capacity],
                                tDeparture.[Destination] AS TragerDepartureDestination,
                                tArriving.[Destination] AS TragerArrivingDestination,
                                v.[Number] AS VehicleNumber, v.[Brand] AS VehicleBrand, v.[Model] AS VehicleModel,
                                o.[status],
                                CASE 
                                    WHEN o.[status] IS NULL THEN 'Non validé'
                                    WHEN o.[status] = 0 THEN 'Non validé'
                                    WHEN o.[status] = 1 THEN 'Validé'
                                    WHEN o.[status] = 2 THEN 'En cours'
                                    WHEN o.[status] = 3 THEN 'Terminé'
                                    ELSE 'Inconnu'
                                END AS StatusName
                            FROM [Offer] o
                            LEFT JOIN [NosTrager] tDeparture ON o.[idTragerDeparture] = tDeparture.[Id]
                            LEFT JOIN [NosTrager] tArriving ON o.[idTragerArriving] = tArriving.[Id]
                            LEFT JOIN [Vehicle] v ON o.[idVehicle] = v.[Id]";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine("----offer");
                        var offer = new Offer
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdTragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                            IdTragerArriving = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                            IdClient = reader["idClient"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idClient"]),
                            IdVehicle = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                            OtherTragerDescription = reader["otherTragerDescription"] == DBNull.Value ? null : reader["otherTragerDescription"].ToString(),
                            Description = reader["description"] == DBNull.Value ? null : reader["description"].ToString(),
                            DateMission = Convert.ToDateTime(reader["dateMission"]),
                            TotalAmount = reader["totalAmount"] == DBNull.Value ? null : (decimal?)reader["totalAmount"],
                            CreatedAt = reader["created_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["created_at"]),
                            Capacity = reader["capacity"] == DBNull.Value ? null : (decimal?)reader["capacity"],
                            status = reader["status"] == DBNull.Value ? 0 : Convert.ToInt32(reader["status"]),
                            statusName = reader["StatusName"].ToString(),
                            TragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                                Destination = reader["TragerDepartureDestination"] == DBNull.Value ? null : reader["TragerDepartureDestination"].ToString()
                            },
                            TragerArriving = reader["idTragerArriving"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                                Destination = reader["TragerArrivingDestination"] == DBNull.Value ? null : reader["TragerArrivingDestination"].ToString()
                            },
                            Vehicle = reader["idVehicle"] == DBNull.Value ? null : new Vehicle
                            {
                                Id = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                                Number = reader["VehicleNumber"] == DBNull.Value ? null : reader["VehicleNumber"].ToString(),
                                Brand = reader["VehicleBrand"] == DBNull.Value ? null : reader["VehicleBrand"].ToString(),
                                Model = reader["VehicleModel"] == DBNull.Value ? null : reader["VehicleModel"].ToString()
                            }
                        };
                        offers.Add(offer);
                    }
                    Console.WriteLine("+++offers" + offers.Count());
                }
                catch (SqlException ex)
                {
                    // Log de l'exception (à remplacer par un logger dans un projet réel)
                    Console.WriteLine("Erreur SQL : " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Log d'autres types d'erreurs
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            return offers;
        }

        public async Task<bool> AddOffre(Offer data)
        {
            try
            {
                Console.WriteLine("Email: " + data.Email);
                Console.WriteLine("NameSociete: " + data.NameSociete);
                Console.WriteLine("IdTragerArriving: " + data.IdTragerArriving);
                Console.WriteLine("IdTragerDeparture: " + data.IdTragerDeparture);
                Console.WriteLine("IdVehicle: " + data.IdVehicle);
                Console.WriteLine("Description: " + data.Description);
                Console.WriteLine("OtherTragerDescription: " + data.OtherTragerDescription);
                Console.WriteLine("Capacity: " + data.Capacity);
                Console.WriteLine("TotalAmount: " + data.TotalAmount);
                Console.WriteLine("DateMission: " + data.DateMission);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                INSERT INTO [Offer] 
                (idTragerDeparture, idClient, idVehicle, otherTragerDescription, description, dateMission, totalAmount, created_at, idTragerArriving, capacity, NameSociete, Email,status) 
                VALUES 
                (@idTragerDeparture, @idClient, @idVehicle, @otherTragerDescription, @description, @dateMission, @totalAmount, @created_at, @idTragerArriving, @capacity, @NameSociete, @Email,@status)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ajout des paramètres pour éviter les injections SQL
                        command.Parameters.AddWithValue("@idTragerDeparture", data.IdTragerDeparture ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idClient", data.IdClient ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idVehicle", data.IdVehicle ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@otherTragerDescription", string.IsNullOrEmpty(data.OtherTragerDescription) ? DBNull.Value : data.OtherTragerDescription);
                        command.Parameters.AddWithValue("@description", string.IsNullOrEmpty(data.Description) ? DBNull.Value : data.Description);
                        command.Parameters.AddWithValue("@dateMission", data.DateMission);
                        command.Parameters.AddWithValue("@totalAmount", data.TotalAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idTragerArriving", data.IdTragerArriving ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@capacity", data.Capacity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NameSociete", data.NameSociete ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", data.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@status", data.status ?? (object)DBNull.Value);
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

        public async Task<List<Offer>> getOffreByIdClientAsync(int? idClient)
        {
            var offers = new List<Offer>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                            SELECT 
                                o.[Id], o.[idTragerDeparture], o.[idClient], o.[idVehicle], 
                                o.[otherTragerDescription], o.[description], o.[dateMission], 
                                o.[totalAmount], o.[created_at], o.[idTragerArriving], o.[capacity],
                                tDeparture.[Destination] AS TragerDepartureDestination,
                                tArriving.[Destination] AS TragerArrivingDestination,
                                v.[Number] AS VehicleNumber, v.[Brand] AS VehicleBrand, v.[Model] AS VehicleModel,
                                o.[status],
                                CASE 
                                    WHEN o.[status] IS NULL THEN 'Non validé'
                                    WHEN o.[status] = 0 THEN 'Non validé'
                                    WHEN o.[status] = 1 THEN 'Validé'
                                    WHEN o.[status] = 2 THEN 'En cours'
                                    WHEN o.[status] = 3 THEN 'Terminé'
                                    ELSE 'Inconnu'
                                END AS StatusName
                            FROM [Offer] o
                            LEFT JOIN [NosTrager] tDeparture ON o.[idTragerDeparture] = tDeparture.[Id]
                            LEFT JOIN [NosTrager] tArriving ON o.[idTragerArriving] = tArriving.[Id]
                            LEFT JOIN [Vehicle] v ON o.[idVehicle] = v.[Id]
                            WHERE o.[idClient] = @IdClient";

                try
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdClient", idClient ?? (object)DBNull.Value);
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var offer = new Offer
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdTragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                            IdTragerArriving = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                            IdClient = reader["idClient"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idClient"]),
                            IdVehicle = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                            OtherTragerDescription = reader["otherTragerDescription"] == DBNull.Value ? null : reader["otherTragerDescription"].ToString(),
                            Description = reader["description"] == DBNull.Value ? null : reader["description"].ToString(),
                            DateMission = Convert.ToDateTime(reader["dateMission"]),
                            TotalAmount = reader["totalAmount"] == DBNull.Value ? null : (decimal?)reader["totalAmount"],
                            CreatedAt = reader["created_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["created_at"]),
                            Capacity = reader["capacity"] == DBNull.Value ? null : (decimal?)reader["capacity"],
                            status = reader["status"] == DBNull.Value ? 0 : Convert.ToInt32(reader["status"]),
                            statusName = reader["StatusName"].ToString(),
                            TragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                                Destination = reader["TragerDepartureDestination"] == DBNull.Value ? null : reader["TragerDepartureDestination"].ToString()
                            },
                            TragerArriving = reader["idTragerArriving"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                                Destination = reader["TragerArrivingDestination"] == DBNull.Value ? null : reader["TragerArrivingDestination"].ToString()
                            },
                            Vehicle = reader["idVehicle"] == DBNull.Value ? null : new Vehicle
                            {
                                Id = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                                Number = reader["VehicleNumber"] == DBNull.Value ? null : reader["VehicleNumber"].ToString(),
                                Brand = reader["VehicleBrand"] == DBNull.Value ? null : reader["VehicleBrand"].ToString(),
                                Model = reader["VehicleModel"] == DBNull.Value ? null : reader["VehicleModel"].ToString()
                            }
                        };
                        offers.Add(offer);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return offers;
        }

        public decimal calculeMontant(decimal capacity, decimal defaultMontant)
        {
            decimal resultMontant = defaultMontant;

            if (capacity <= 1)
            {
                resultMontant = defaultMontant * 0.8m;
            }
            else if (capacity > 1 && capacity <= 3)
            {
                resultMontant = defaultMontant;
            }
            else if (capacity > 3 && capacity <= 10)
            {
                resultMontant = defaultMontant * 1.2m;
            }
            else if (capacity > 10)
            {
                resultMontant = defaultMontant * 1.5m;
            }

            return resultMontant;
        }

        public async Task<List<Offer>> getAllOffreNonValiderAsync()
        {
            var offers = new List<Offer>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                            SELECT 
                                o.[Id], o.[idTragerDeparture], o.[idClient], o.[idVehicle], 
                                o.[otherTragerDescription], o.[description], o.[dateMission], 
                                o.[totalAmount], o.[created_at], o.[idTragerArriving], o.[capacity],
                                tDeparture.[Destination] AS TragerDepartureDestination,
                                tArriving.[Destination] AS TragerArrivingDestination,
                                v.[Number] AS VehicleNumber, v.[Brand] AS VehicleBrand, v.[Model] AS VehicleModel,
                                o.[status],
                                CASE 
                                    WHEN o.[status] IS NULL THEN 'Non validé'
                                    WHEN o.[status] = 0 THEN 'Non validé'
                                    WHEN o.[status] = 1 THEN 'Validé'
                                    WHEN o.[status] = 2 THEN 'En cours'
                                    WHEN o.[status] = 3 THEN 'Terminé'
                                    ELSE 'Inconnu'
                                END AS StatusName
                            FROM [Offer] o
                            LEFT JOIN [NosTrager] tDeparture ON o.[idTragerDeparture] = tDeparture.[Id]
                            LEFT JOIN [NosTrager] tArriving ON o.[idTragerArriving] = tArriving.[Id]
                            LEFT JOIN [Vehicle] v ON o.[idVehicle] = v.[Id]
                            WHERE o.[status] <= 0";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine("----offer");
                        var offer = new Offer
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdTragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                            IdTragerArriving = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                            IdClient = reader["idClient"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idClient"]),
                            IdVehicle = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                            OtherTragerDescription = reader["otherTragerDescription"] == DBNull.Value ? null : reader["otherTragerDescription"].ToString(),
                            Description = reader["description"] == DBNull.Value ? null : reader["description"].ToString(),
                            DateMission = Convert.ToDateTime(reader["dateMission"]),
                            TotalAmount = reader["totalAmount"] == DBNull.Value ? null : (decimal?)reader["totalAmount"],
                            CreatedAt = reader["created_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["created_at"]),
                            Capacity = reader["capacity"] == DBNull.Value ? null : (decimal?)reader["capacity"],
                            status = reader["status"] == DBNull.Value ? 0 : Convert.ToInt32(reader["status"]),
                            statusName = reader["StatusName"].ToString(),
                            TragerDeparture = reader["idTragerDeparture"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerDeparture"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerDeparture"]),
                                Destination = reader["TragerDepartureDestination"] == DBNull.Value ? null : reader["TragerDepartureDestination"].ToString()
                            },
                            TragerArriving = reader["idTragerArriving"] == DBNull.Value ? null : new NosTrager
                            {
                                Id = reader["idTragerArriving"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTragerArriving"]),
                                Destination = reader["TragerArrivingDestination"] == DBNull.Value ? null : reader["TragerArrivingDestination"].ToString()
                            },
                            Vehicle = reader["idVehicle"] == DBNull.Value ? null : new Vehicle
                            {
                                Id = reader["idVehicle"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idVehicle"]),
                                Number = reader["VehicleNumber"] == DBNull.Value ? null : reader["VehicleNumber"].ToString(),
                                Brand = reader["VehicleBrand"] == DBNull.Value ? null : reader["VehicleBrand"].ToString(),
                                Model = reader["VehicleModel"] == DBNull.Value ? null : reader["VehicleModel"].ToString()
                            }
                        };
                        offers.Add(offer);
                    }
                    Console.WriteLine("+++offers" + offers.Count());
                }
                catch (SqlException ex)
                {
                    // Log de l'exception (à remplacer par un logger dans un projet réel)
                    Console.WriteLine("Erreur SQL : " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Log d'autres types d'erreurs
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            return offers;
        }

        public async Task<bool> UpdateOffreStatus(int id, int newStatus)
        {
            try
            {
                // Log des informations
                Console.WriteLine("ID de l'offre à mettre à jour : " + id);
                Console.WriteLine("Nouveau status : " + newStatus);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                UPDATE [Offer]
                SET status = @newStatus
                WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ajout des paramètres pour éviter les injections SQL
                        command.Parameters.AddWithValue("@newStatus", newStatus);
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0; // Retourne vrai si la mise à jour a réussi
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

        public async Task<bool> DeleteOfferByIdAsync(int idOffer)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [Offer] WHERE [Id] = @IdOffer";

                try
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdOffer", idOffer);

                    await con.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    // Si au moins une ligne est supprimée, retourne true
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }


    }
}
