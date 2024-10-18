using Newtonsoft.Json;

namespace Ester.Sms.Sender.Models
{
    public class ChargeRequest
    {
        [JsonProperty("MSISDN")]
        public string MSISDN { get; set; }

        [JsonProperty("PRODUCT_CATEGORY")]
        public string ProductCategory { get; set; }

        [JsonProperty("SUB_PRODUCT_ID")]
        public string? SubProductId { get; set; }

        [JsonProperty("ASSET_PRICE_BAND")]
        public string? AssetPriceBand { get; set; }

        [JsonProperty("ASSET_BASE_PRICE")]
        public double? AssetBasePrice { get; set; }

        [JsonProperty("ASSET_BASE_UNIT_PRICE")]
        public double? AssetBaseUnitPrice { get; set; }

        [JsonProperty("CURRENCY_CODE")]
        public string? CurrencyCode { get; set; }

        [JsonProperty("VAT_CODE")]
        public string? VatCode { get; set; }

        [JsonProperty("REQUEST_TIME")]
        public string RequestTime { get; set; }

        [JsonProperty("ACCESS_METHOD")]
        public string? AccessMethod { get; set; }

        [JsonProperty("PAYMENT_METHOD")]
        public string? PaymentMethod { get; set; }

        [JsonProperty("UNITS")]
        public string Units { get; set; }

        [JsonProperty("ASSET_ID")]
        public string? AssetId { get; set; }

        [JsonProperty("PAYER_ACCOUNT_ID")]
        public string? PayerAccountId { get; set; }

        [JsonProperty("RBID")]
        public string? Rbid { get; set; }

        [JsonProperty("ASSET_PRICE")]
        public AssetPrice? AssetPrice { get; set; }

        [JsonProperty("ADDITTORY")]
        public AddittoryList? Addittory { get; set; }

        [JsonProperty("ASSET_UNIT_PRICE")]
        public AssetUnitPrice? AssetUnitPrice { get; set; }

        [JsonProperty("ROID")]
        public Roid? Roid { get; set; }

        //response
        public ResponseModel? Response { get; set; }


    }
}
