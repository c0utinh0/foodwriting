using FluentMigrator.Runner;
using FoodWriting.Infrastructure.Migrations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentMigratorCore().ConfigureRunner(c => c.AddMySql5()
.WithGlobalConnectionString(builder.Configuration.GetConnectionString("Connection")).ScanIn(Assembly.Load("FoodWriting.Infrastructure")).For.All()
);

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

DatabaseUpdate();

app.Run();

void DatabaseUpdate()
{
    var connection = builder.Configuration.GetConnectionString("Connection");
    var database = builder.Configuration.GetConnectionString("Database");

    Database.CreateDatabase(connection, database);

    app.MigrateDatabase();
}
