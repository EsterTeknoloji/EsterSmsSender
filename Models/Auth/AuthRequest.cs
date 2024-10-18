using Newtonsoft.Json;

namespace Ester.Sms.Sender.Models.Auth;

public class AuthRequest
{
    [JsonProperty("serviceVariantId")]
    public string VariantId { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("spId")]
    public string UserName { get; set; }
}
