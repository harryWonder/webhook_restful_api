namespace webhook_restful_api.Models;

public class MongoDatabaseSetting
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string WebhookHashCollection { get; set; }
}
