using Reto_0_Backend;
using Reto_0_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

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