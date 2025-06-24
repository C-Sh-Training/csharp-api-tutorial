using CSharp_Tutorial_Repositories.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config database

builder.Services.AddDbContext<BookManagementDbContext>(options =>
{
    // Use the connection string from appsettings.json or environment variables
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookDbLocal"));
});

// Register repositories and services (assuming you have these interfaces and implementations)


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
