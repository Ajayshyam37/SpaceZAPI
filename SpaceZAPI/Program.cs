using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpaceZAPI.Models;
using SpaceZAPI.Services;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.Configure<SpaceZDatabase>(builder.Configuration.GetSection(nameof(SpaceZDatabase)));
builder.Services.AddSingleton<ISpaceZDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SpaceZDatabase>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("SpaceZDatabase:ConnectionString")));
builder.Services.AddScoped<ISpaceCraftService, SpaceCraftService>();
builder.Services.AddScoped<IPayLoadService, PayLoadService>();
builder.Services.AddScoped<ITelemetryCommunicationService, TelemetryCommunicationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    var frontendurl = configuration.GetValue<string>("frontend_url");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendurl).AllowAnyMethod().AllowAnyHeader();

    });
});

var app = builder.Build();

// In general

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

