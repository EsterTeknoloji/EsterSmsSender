using Newtonsoft.Json;

namespace Ester.Sms.Sender.Models.Auth;

public class AuthResponse
{
    [JsonProperty("sessionId")]
    public string? SessionId { get; set; }

    [JsonProperty("faultactor")]
    public string? FaultActor { get; set; }

    [JsonProperty("faultcode")]
    public string? FaultCode { get; set; }

    [JsonProperty("faultstring")]
    public string? FaultString { get; set; }
}
