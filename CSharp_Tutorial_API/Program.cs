using CSharp_Tutorial_Repositories.DbContext;
using CSharp_Tutorial_Repositories.Repositories;
using CSharp_Tutorial_Services.Mappers;
using CSharp_Tutorial_Services.Services;
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
    //options.UseSqlServer(builder.Configuration.GetConnectionString("BookDbLocal"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookDbVps"));
});

// config automapper
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

// Register repositories and services (assuming you have these interfaces and implementations)
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

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
