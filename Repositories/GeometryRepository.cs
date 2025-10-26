using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public class GeometryRepository : IGeometryRepository
    {

        private readonly string _connectionString;

        public GeometryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Geometry>> GetAllAsync()
        {
            var geometries = new List<Geometry>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Type, Longitude, Latitude FROM Geometries";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var geometry = new Geometry
                            {
                                id = reader.GetString(0),
                                type = reader.GetString(1),
                                coordinates = new double[] { reader.GetDouble(2), reader.GetDouble(3) }
                            };

                            geometries.Add(geometry);
                        }
                }
            }
            return geometries;
        }


        public async Task<Geometry> GetByIdAsync(string id)
        {
            Geometry geometry = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Type, Longitude, Latitude FROM Geometries WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            geometry = new Geometry
                            {
                                id = reader.GetString(0),
                                type = reader.GetString(1),
                                coordinates = new double[] { reader.GetDouble(2), reader.GetDouble(3) }
                            };
                        }
                    }
                }
            }
            return geometry;
        }

        public async Task AddAsync(Geometry geometry)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Geometries (Id, Type, Longitude, Latitude) VALUES (@IdGeometry, @TypeGeometry, @LongitudeGeometry, @LatitudeGeometry)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGeometry", geometry.id);
                    command.Parameters.AddWithValue("@TypeGeometry", geometry.type);
                    command.Parameters.AddWithValue("@LongitudeGeometry", geometry.coordinates[0]);
                    command.Parameters.AddWithValue("@LatitudeGeometry", geometry.coordinates[1]);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Geometry geometry)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Geometries SET Type = @TypeGeometry, Longitude = @LongitudeGeometry, Latitude = @LatitudeGeometry WHERE Id = @IdGeometry";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGeometry", geometry.id);
                    command.Parameters.AddWithValue("@TypeGeometry", geometry.type);
                    command.Parameters.AddWithValue("@LongitudeGeometry", geometry.coordinates[0]);
                    command.Parameters.AddWithValue("@LatitudeGeometry", geometry.coordinates[1]);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Geometries WHERE Id = @IdGeometry";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGeometry", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        //métodos para tablas cruzadas
        public async Task<List<Geometry>> GetAllGeometryByEventAsync(string eventId)
        {
            var geometries = new List<Geometry>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT GeometryId FROM EventGeometries WHERE EventId = @EventId ORDER BY SequenceOrder";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Geometry geometry = await GetByIdAsync(reader.GetString(0));
                            geometries.Add(geometry);
                        }
                    }
                }
            }
            return geometries;
        }
    }
}