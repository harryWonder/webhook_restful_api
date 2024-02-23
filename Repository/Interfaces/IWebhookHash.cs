using MongoDB.Bson;
using webhook_restful_api.Models;

namespace webhook_restful_api.Repository.Interfaces;

public interface IWebhookHash
{
    public Task<WebhookHash> CreateHashAsync(WebhookHash webhookHash);

    public Task<WebhookHash> UpdateHasAsync(WebhookHash condition, WebhookHash payload);

    public Task<WebhookHash> FindHashAsync(ObjectId hash);

    public Task<WebhookHash> FindHashWithReferenceAsync(string reference);

}
