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
    options.UseNpgsql(builder.Configuration.GetConnectionString("BookDbLocal"));
});

// Register repositories and services (assuming you have these interfaces and implementations)
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Register AutoMapper (if you are using it for mapping)    
 builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly); 

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
