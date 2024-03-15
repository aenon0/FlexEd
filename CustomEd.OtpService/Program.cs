using Swashbuckle.AspNetCore.SwaggerUI;
using CustomEd.OtpService.Repository;
using CustomEd.OtpService.Service;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();


var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MongoConnectionString");
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient(connectionString);
});


var databaseName = configuration.GetValue<string>("MongoDatabaseName");

builder.Services.AddScoped(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase(databaseName);
    var collectionNames = database.ListCollectionNames().ToList();
    // if (!collectionNames.Contains("Package"))
    // {
    //     // Create the collection if it doesn't exist
    //     database.CreateCollection("Package");
    // };
    return database;
});



builder.Services.AddScoped<IOtpRepository, OtpRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rideshare API V1");
   c.RoutePrefix = "swagger"; // This will set the swagger UI route to 'http://localhost:8080/swagger'
   c.DocExpansion(DocExpansion.None);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
