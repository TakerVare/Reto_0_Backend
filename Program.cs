using Reto_0_Backend;
using Reto_0_Backend.Models;
using Reto_0_Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EonetDB");

// =============================================
// REGISTRAR REPOSITORIOS
// =============================================

// Repositorios sin dependencias
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(provider =>
    new CategoryRepository(connectionString));

builder.Services.AddScoped<ISourceRepository, SourceRepository>(provider =>
    new SourceRepository(connectionString));

builder.Services.AddScoped<IGeometryRepository, GeometryRepository>(provider =>
    new GeometryRepository(connectionString));

builder.Services.AddScoped<ILayerParametersRepository, LayerParametersRepository>(provider =>
    new LayerParametersRepository(connectionString));

// Repositorios con dependencias
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>(provider =>
{
    var categoryRepo = provider.GetRequiredService<ICategoryRepository>();
    var sourceRepo = provider.GetRequiredService<ISourceRepository>();
    return new PropertyRepository(connectionString, categoryRepo, sourceRepo);
});

builder.Services.AddScoped<IEventRepository, EventRepository>(provider =>
{
    var categoryRepo = provider.GetRequiredService<ICategoryRepository>();
    var sourceRepo = provider.GetRequiredService<ISourceRepository>();
    var geometryRepo = provider.GetRequiredService<IGeometryRepository>();
    return new EventRepository(connectionString, categoryRepo, sourceRepo, geometryRepo);
});

builder.Services.AddScoped<IFeatureRepository, FeatureRepository>(provider =>
{
    var propertyRepo = provider.GetRequiredService<IPropertyRepository>();
    var geometryRepo = provider.GetRequiredService<IGeometryRepository>();
    return new FeatureRepository(connectionString, propertyRepo, geometryRepo);
});

builder.Services.AddScoped<ILayerRepository, LayerRepository>(provider =>
{
    var layerParamsRepo = provider.GetRequiredService<ILayerParametersRepository>();
    return new LayerRepository(connectionString, layerParamsRepo);
});

// Añadir política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins(
                    "http://127.0.0.1:5500",
                    "http://localhost:5500"
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();

// Registrar DataCollectionExample como Singleton para compartir datos entre controladores
builder.Services.AddSingleton<DataCollectionExample>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comentar si usas HTTP
// app.UseHttpsRedirection();

// ✅ CRÍTICO: Activar CORS
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();