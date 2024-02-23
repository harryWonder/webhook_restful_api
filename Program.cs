using MongoDB.Driver;
using webhook_restful_api.Models;
using webhook_restful_api.Repository;
using webhook_restful_api.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Map The Object In The Config To MongoDatabaseSetting Object
builder.Services.Configure<MongoDatabaseSetting>(
   builder.Configuration.GetSection("MongoDatabaseSettings")
);

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = builder.Configuration.GetSection("MongoDatabaseSettings:ConnectionString")
        .Value;

    return new MongoClient(connectionString);
});

// Map The Interfaces To Their Concrete Classes...
builder.Services.AddScoped<IWebhookHash, WebhookHashRepository>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
