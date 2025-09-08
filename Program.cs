var builder = WebApplication.CreateBuilder(args);

// Registramos servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // para Swagger

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // habilita Swagger
}

app.UseHttpsRedirection();

// Esto permite que los endpoints de tus controllers funcionen
app.MapControllers();

app.Run();