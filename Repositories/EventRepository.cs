using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class EventRepository : IEventRepository
    {

        private readonly string _connectionString;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IGeometryRepository _geometryRepository;

        public EventRepository(string connectionString, 
                             ICategoryRepository categoryRepository,
                             ISourceRepository sourceRepository,
                             IGeometryRepository geometryRepository)
        {
            _connectionString = connectionString;
            _categoryRepository = categoryRepository;
            _sourceRepository = sourceRepository;
            _geometryRepository = geometryRepository;
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
                                categories = await _categoryRepository.GetAllCategoriesByEventAsync(reader.GetString(0)),                                sources = await _sourceRepository.GetAllSourcesByEventAsync(reader.GetString(0)),
                                geometry = await _geometryRepository.GetAllGeometryByEventAsync(reader.GetString(0))                                
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
                                categories = await _categoryRepository.GetAllCategoriesByEventyAsync(reader.GetString(0)),
                                sources = await _sourceRepository.GetAllSourcesByEventAsync(reader.GetString(0)),
                                geometry = await _geometryRepository.GetAllGeometryByEventAsync(reader.GetString(0)) 
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

                string query = "INSERT INTO Events (Id, Title, DescriptionEvent, Link, Closed) VALUES (@Id, @Title, @DescriptionEvent, @Link, @Closed)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", evento.id);
                    command.Parameters.AddWithValue("@Title", evento.title);
                    command.Parameters.AddWithValue("@DescriptionEvent", evento.description);
                    command.Parameters.AddWithValue("@Link", evento.link);
                    command.Parameters.AddWithValue("@Closed", evento.closed);
                    //Pendiente de implementar métodos para insertar las listas de categorías, fuentes y geometrías
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Evento evento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Events SET Title = @Title, DescriptionEvent = @DescriptionEvent, Link = @Link, Closed = @Closed WHERE IdCategory = @IdCategory";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", evento.id);
                    command.Parameters.AddWithValue("@Title", evento.title);
                    command.Parameters.AddWithValue("@DescriptionEvent", evento.description);
                    command.Parameters.AddWithValue("@Link", evento.link);
                    command.Parameters.AddWithValue("@Closed", evento.closed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Events WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /*
        Aquí irán los métodos para tablas cruzadas crear registros en las tablas intermedias entre eventos y categorías, fuentes y geometrías
        
        */

    }




}