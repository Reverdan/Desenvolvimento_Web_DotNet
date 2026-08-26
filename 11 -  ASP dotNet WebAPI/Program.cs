var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Permite que o cliente HTML/JS do exercício 13, servido em outra origem, chame este endpoint.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapControllers();

app.Run();
