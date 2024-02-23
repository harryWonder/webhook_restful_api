using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using webhook_restful_api.Models;
using webhook_restful_api.Repository.Interfaces;

namespace webhook_restful_api.Repository;

public class WebhookHashRepository : IWebhookHash
{

    public readonly IMongoCollection<WebhookHash> _webhookHashCollection;

    public WebhookHashRepository(IOptions<MongoDatabaseSetting> mongoDatabaseSettings, IMongoClient client)
    {
        var database = client.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
        _webhookHashCollection = database.GetCollection<WebhookHash>(mongoDatabaseSettings.Value.WebhookHashCollection);
    }

    public async Task<WebhookHash> CreateHashAsync(WebhookHash webhookHash)
    {
        var objectId = ObjectId.GenerateNewId();

        webhookHash.Id = objectId;
        await _webhookHashCollection.InsertOneAsync(webhookHash);

        return webhookHash;
    }

    public Task<WebhookHash> FindHashAsync(ObjectId hash)
    {
        return _webhookHashCollection.Find(webhookHash => webhookHash.Id == hash)
                .FirstOrDefaultAsync();
    }

    public Task<WebhookHash> FindHashWithReferenceAsync(string reference)
    {
        return _webhookHashCollection.Find(webhookHash => webhookHash.Reference == reference)
                .FirstOrDefaultAsync();
    }

    public async Task<WebhookHash> UpdateHasAsync(WebhookHash condition, WebhookHash payload)
    {
        await _webhookHashCollection.FindOneAndReplaceAsync(webhookhash => webhookhash.Id == condition.Id, payload);

        return payload;
    }
}