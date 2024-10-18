using Newtonsoft.Json;

namespace Ester.Sms.Sender.Models
{
    public class ResponseModel
    {

        [JsonProperty("TSOresult")]
        public TSOresponse TSOresponse { get; set; }

        [JsonProperty("CURRENCY_CODE")]
        public string CurrencyCode { get; set; }

        [JsonProperty("FINAL_PRICE")]
        public FinalPrice FinalPrice { get; set; }

        [JsonProperty("ROID")]
        public Roid? Roid { get; set; }
    }

    public partial class Roid
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class FinalPrice
    {
        [JsonProperty("price")]
        public string Price { get; set; }
    }

    public partial class TSOresponse
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("errorCode")]
        public string? ErrorCode { get; set; }

        [JsonProperty("errorDescription")]
        public string? ErrorDescription { get; set; }

    }
}
