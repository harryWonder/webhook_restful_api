using MongoDB.Bson;
using webhook_restful_api.Models;

namespace webhook_restful_api.Repository.Interfaces;

public interface IIncomingRequest
{

    Task<IncomingRequest> CreateRequestAsync(IncomingRequest incomingRequest);

    Task<IncomingRequest> UpdateRequestAsync(IncomingRequest condition, IncomingRequest payload);

    Task<IncomingRequest> FindOneRequestAsync(ObjectId requestId);

    Task<List<IncomingRequest>> FindRequestsAsync(ObjectId webhookId);
}