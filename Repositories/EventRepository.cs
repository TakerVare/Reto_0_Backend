using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class EventRepository : IEventRepository
    {

        private readonly string _connectionString;

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Evento>> GetAllAsync()
        {
            var events = new List<Evento>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Title, Description, Link, Closed FROM Events";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var evento = new Evento
                            {
                                id = reader.GetString(0),
                                title = reader.GetString(1),
                                description = reader.GetString(2),
                                link = reader.GetString(3),
                                closed = reader.GetString(4),
                                categories = await GetAllCategoriesByEventyAsync(reader.GetString(0)),
                                //Todo
                                /*
                                categories = getCategoriesByEventId()
                                sources = getSourcesByEventId()
                                geometry = getGeometryByEventId()
                                */
                            };

                            events.Add(evento);
                        }
                }
            }
            return events;


        }
        

        public async Task<Evento> GetByIdAsync(string id)
        {
            Evento evento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Title, Description, Link, Closed FROM Events WHERE EventId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            evento = new Evento
                            {
                                id = reader.GetString(0),
                                title = reader.GetString(1),
                                description = reader.GetString(2),
                                link = reader.GetString(3),
                                closed = reader.GetString(4),
                                categories = await GetAllCategoriesByEventyAsync(reader.GetString(0)),
                                
                                sources = getSourcesByEventId()
                                geometry = getGeometryByEventId()
                                */
                            };   
                            
                        }
                    }
                }
            }
            return evento;
        }

        public async Task AddAsync(Evento evento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Categories (IdCategory, TitleCategory, LinkCategory, DescriptionCategory, LayersCategory) VALUES (@IdCategory, @TitleCategory, @LinkCategory, @DescriptionCategory, @LayersCategory)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", category.idCategory);
                    command.Parameters.AddWithValue("@TitleCategory", category.titleCategory);
                    command.Parameters.AddWithValue("@LinkCategory", category.linkCategory);
                    command.Parameters.AddWithValue("@DescriptionCategory", category.descriptionCategory);
                    command.Parameters.AddWithValue("@LayersCategory", category.layersCategory);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Categories SET TitleCategory = @TitleCategory, LinkCategory = @LinkCategory, DescriptionCategory = @DescriptionCategory, LayersCategory = @LayersCategory WHERE IdCategory = @IdCategory";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", category.idCategory);
                    command.Parameters.AddWithValue("@TitleCategory", category.titleCategory);
                    command.Parameters.AddWithValue("@LinkCategory", category.linkCategory);
                    command.Parameters.AddWithValue("@DescriptionCategory", category.descriptionCategory);
                    command.Parameters.AddWithValue("@LayersCategory", category.layersCategory);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Categories WHERE IdCategory = @IdCategory";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /*
        Cambio de idea, se llamará a estos métodos desde sus correspondientes repositorios
        public async Task<List<Category>> getCategoriesByEventId(string eventId)
        {
            //Implementar metodo para obtener las categorias de un evento
            throw new NotImplementedException();
        }

        public async Task<List<Source>> getSourcesByEventId(string eventId)
        {
            //Implementar metodo para obtener las categorias de un evento
            throw new NotImplementedException();
        }
        
        public async Task<List<Geometry>> getGeometryByEventId(string eventId)
        {
            //Implementar metodo para obtener las categorias de un evento
            throw new NotImplementedException();
        }

        */

    }




}