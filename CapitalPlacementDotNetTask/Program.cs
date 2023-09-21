﻿using CapitalPlacementDotNetTask.Data;
using CapitalPlacementDotNetTask.EndPoints;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

// Adding the CosmosDB connection string to the services.
var connectionString = builder.Configuration.GetConnectionString("CosmosDB");
var databaseName = builder.Configuration["DatabaseName"];

builder.Services.AddDbContext<CosmosDataBaseContext>(options =>
{
    if (connectionString is not null && databaseName is not null)
        options.UseCosmos(connectionString, databaseName);
    else throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
});

// Adding the FluentValidation to the services.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

// mapping the endpoints of "Tap 1" [POST][GET][PUT].
app.MapProgramDetailsEndPoints();




app.UseHttpsRedirection();
app.Run();

