using FluentMigrator.Runner;
using FoodWriting.Infrastructure.Migrations;
using System.Reflection;
using FoodWriting.Domain.Interfaces;
using FoodWriting.Infrastructure;
using FoodWriting.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var versionServer = new MySqlServerVersion(new Version(8, 3, 0));
var conectionString = builder.Configuration.GetConnectionString("Connection");
var database = builder.Configuration.GetConnectionString("Database");


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentMigratorCore().ConfigureRunner(c => c.AddMySql5()
.WithGlobalConnectionString(conectionString).ScanIn(Assembly.Load("FoodWriting.Infrastructure")).For.All()
);

builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddDbContext<DataContext>(options => options.
    UseMySql(conectionString, versionServer));

//Dependency Injection

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();


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
    Database.CreateDatabase(conectionString, database);

    app.MigrateDatabase();
}
