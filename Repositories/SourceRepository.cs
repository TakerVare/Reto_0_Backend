using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class SourceRepository : ISourceRepository
    {

        private readonly string _connectionString;

        public SourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Source>> GetAllAsync()
        {
            var sources = new List<Source>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Url FROM Sources";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var source = new Source
                            {
                                id = reader.GetString(0),
                                url = reader.GetString(1)
                            };

                            sources.Add(source);
                        }
                }
            }
            return sources;


        }
        

        public async Task<Source> GetByIdAsync(string id)
        {
            Source source = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Url FROM Sources WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            source = new Source
                            {
                                id = reader.GetString(0),
                                url = reader.GetString(1)
                            };   
                            
                        }
                    }
                }
            }
            return source;
        }

        public async Task AddAsync(Source source)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Sources (Id, Url) VALUES (@IdSource, @UrlSource)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSource", source.id);
                    command.Parameters.AddWithValue("@UrlSource", source.url);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Source source)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Sources SET Url = @UrlSource WHERE Id = @IdSource";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSource", source.id);
                    command.Parameters.AddWithValue("@UrlSource", source.url);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Sources WHERE Id = @IdGeometry";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSource", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        // MÉTODOS PARA TABLAS CRUZADAS

        // PropertySources
        public async Task<List<Source>> GetAllSourcesByPropertyAsync(string propertyId)
        {
            var sources = new List<Source>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT SourceId FROM PropertySources WHERE PropertyId = @PropertyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", propertyId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Source source = await GetByIdAsync(reader.GetString(0));
                            sources.Add(source);
                               
                            
                        }
                    }
                }
            }
            return sources;
        }

        


        

    }




}