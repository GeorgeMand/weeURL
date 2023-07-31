using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using weeURL.Records;
using weeURL.Models;
using static weeURL.HashFunctions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoClient>(_ => new MongoClient());
builder.Services.Configure<WeeUrlDatabaseSettings>(
    builder.Configuration.GetSection("WeeDatabase"));
builder.Services.AddSingleton<IMongoDatabase>(
    provider => provider.GetRequiredService<MongoClient>().GetDatabase("Wee"));

builder.Services.AddSingleton<IMongoCollection<URL>>(
    provider => provider.GetRequiredService<IMongoDatabase>().GetCollection<URL>("Records"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/create", async (IMongoCollection < URL > collection, string longURL) =>
{
    var shortURL = createShortUrl(longURL);
    await collection.InsertOneAsync(new URL(shortURL,longURL));
    return TypedResults.Ok(shortURL);
})
.WithName("create")
.WithOpenApi();

app.MapGet("/{shortURL}", async (IMongoCollection<URL> collection, ObjectId shortURL) =>
{
    URL temp = await collection.Find(g => g.shortURL == shortURL.ToString()).FirstOrDefaultAsync();
    return TypedResults.Redirect(temp.longURL, false);
})
.WithName("getLongURL")
.WithOpenApi();


app.Run();
