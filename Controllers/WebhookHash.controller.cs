using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;
using webhook_restful_api.Common.Enums;
using webhook_restful_api.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webhook_restful_api.Controllers
{
    [Route("api/webhook-hash")]
    [ApiController]
    public class WebhookHashController : ControllerBase
    {
        private readonly IWebhookHash _webhookHashRepository;
        private readonly ILogger<WebhookHashController> _logger;

        public WebhookHashController(IWebhookHash webhookHash, ILogger<WebhookHashController> logger)
        {
            _webhookHashRepository = webhookHash;
            _logger = logger;
        }

        // POST: api/create
        [Route("/create"), HttpPost]
        public async Task<IActionResult> Create(string reference)
        {
            try
            {
                if (reference is null || String.IsNullOrEmpty(reference))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Sorry, Your reference property is required!");
                }

                var isReferenceUnique = await _webhookHashRepository.FindHashWithReferenceAsync(reference);
                if (isReferenceUnique is not null)
                {
                    return StatusCode((int)HttpStatusCode.Conflict, "Sorry, You are not allowed to use this reference!");
                }

                var createUniqueHash = System.Guid.NewGuid().ToString();
                var ipAddress = "127.0.0.1";

                var newWebhookhash = await _webhookHashRepository.CreateHashAsync(
                    new Models.WebhookHash
                    {
                        Id = ObjectId.GenerateNewId(),
                        Reference = reference,
                        Hash = createUniqueHash,
                        IpAddress = ipAddress,
                        Status = (int)StatusEnum.Active,
                        LastAccessed = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                    });

                return StatusCode((int)HttpStatusCode.Created, newWebhookhash);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The hash create request failed with message: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Sorry, You just hit a snag! Would you be kind enough to try again!");
            }
        }

        // GET: api/hash
        [Route("/hash"), HttpGet]
        public async Task<IActionResult> Get(string reference)
        {
            try
            {
                if (reference is null || String.IsNullOrEmpty(reference))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Sorry, Your reference property is required!");
                }

                var webhookHash = await _webhookHashRepository.FindHashWithReferenceAsync(reference);

                if (webhookHash is null)
                {
                    return NotFound();
                }

                return StatusCode((int)HttpStatusCode.OK, webhookHash);
            } catch (Exception ex)
            {
                _logger.LogError($"Webhook hash could not be retrieved. Request failed with message: {ex.Message}");

                return StatusCode((int) HttpStatusCode.InternalServerError, "Oops! You just hit a snag! Would you be kind enough to try again?");
            }
        }

    }
}
