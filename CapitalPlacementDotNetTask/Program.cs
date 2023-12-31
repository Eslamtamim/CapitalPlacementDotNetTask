﻿using CapitalPlacementDotNetTask.Data;
using CapitalPlacementDotNetTask.EndPoints;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using CapitalPlacementDotNetTask.Mapping;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

// Adding CosmosDB.
var connectionString = builder.Configuration.GetConnectionString("CosmosDB");
var databaseName = builder.Configuration["DatabaseName"];
builder.Services.AddDbContext<CosmosDataBaseContext>(options =>
{
    if (connectionString is not null && databaseName is not null)
        options.UseCosmos(connectionString, databaseName);
    else throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
});

// Adding AutoMapper.
builder.Services.AddAutoMapper(typeof(MapperConfig));


// Adding FluentValidation.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

// mapping the endpoints of "Program - Tab 1" [POST][GET][PUT].
app.MapProgramDetailsEndPoints();
// mapping the endpoints of "Application Form - Tab 2" [GET][PUT].
app.MapApplicationFormEndPoints();
// mapping the endpoints of "Work Flow - Tab 3" [GET][PUT].
app.MapWorkFlowEndPoints();
// mapping the endpoints of "Preview - Tab 4" [GET].
app.MapProgramPreviewEndPoint();



app.UseHttpsRedirection();
app.Run();

