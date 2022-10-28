namespace Payments.API.Config;

public class StripeSettings
{
    public string SecretKey { get; set; }
    public string PublishableKey { get; set; }
    public string WebhookKey { get; set; }
}