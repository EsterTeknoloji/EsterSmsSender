using Newtonsoft.Json;

namespace Ester.Sms.Sender.Models
{
    public interface ISmsMessage
    {
        [JsonProperty("MESSAGE_BODY")]
        MessageBodies MessageBody { get; set; }

        [JsonProperty("SHORT_NUMBER")]
        string ShortNumber { get; set; }
    }

    public class SendChargedSmsMessage : ISmsMessage
    {
        [JsonProperty("ACCESS_METHOD")]
        public string AccessMethod { get; set; }

        [JsonProperty("ADDITTORY")]
        public AddittoryList? Addittory { get; set; }

        [JsonProperty("ASSET_BASE_PRICE")]
        public string? AssetBasePrice { get; set; }

        [JsonProperty("ASSET_BASE_UNIT_PRICE")]
        public string? AssetBaseUnitPrice { get; set; }

        [JsonProperty("ASSET_ID")]
        public string AssetId { get; set; }

        [JsonProperty("ASSET_PRICE_BAND")]
        public string? AssetPriceBand { get; set; }

        [JsonProperty("ASSET_UNITS")]
        public int AssetUnits { get; set; }

        [JsonProperty("ASSET_PRICE")]
        public AssetPrice? AssetPrice { get; set; }

        [JsonProperty("ASSET_UNIT_PRICE")]
        public AssetUnitPrice? AssetUnitPrice { get; set; }

        [JsonProperty("CID")]
        public string? Cid { get; set; }

        [JsonProperty("CONTENT_SUPPLIER")]
        public string? ContentSupplier { get; set; }

        [JsonProperty("CURRENCY_CODE")]
        public string? CurrencyCode { get; set; }

        [JsonProperty("EXPIRY_DATE")]
        public string? ExpiryDate { get; set; }

        [JsonProperty("INVOICE_TEXT")]
        public string? InvoiceText { get; set; }

        [JsonProperty("IMEI")]
        public string? IMEI { get; set; }

        [JsonProperty("MESSAGE_CLASS")]
        public string? MessageClass { get; set; }

        [JsonProperty("MESSAGE_BODY")]
        public MessageBodies MessageBody { get; set; }

        [JsonProperty("MT_SHORT_NUMBER")]
        public string MtShortNumber { get; set; }

        [JsonProperty("PAYER_ACCOUNT_ID")]
        public string? PayerAccountId { get; set; }

        [JsonProperty("PAYER_MSISDN")]
        public string PayerMsisdn { get; set; }

        [JsonProperty("PAYMENT_METHOD")]
        public string? PaymentMethod { get; set; }

        [JsonProperty("PRODUCT_CATEGORY")]
        public string ProductCategory { get; set; }

        [JsonProperty("SUB_PRODUCT_ID")]
        public string? SubProductId { get; set; }

        [JsonProperty("SENDER")]
        public string? Sender { get; set; }

        [JsonProperty("RBID")]
        public string? Rbid { get; set; }

        [JsonProperty("ROID")]
        public string? Roid { get; set; }

        [JsonProperty("RECEIVER_MSISDN")]
        public string ReceiverMsisdn { get; set; }

        [JsonProperty("REQUEST_TIME")]
        public string RequestTime { get; set; }

        [JsonProperty("S_DATE")]
        public string? SDate { get; set; }

        [JsonProperty("SENDER_MSISDN")]
        public string? SenderMsisdn { get; set; }

        [JsonProperty("VAT_CODE")]
        public string? VatCode { get; set; }

        [JsonProperty("TIMESTAMP")]
        public string? Timestamp { get; set; }

        [JsonProperty("SHORT_NUMBER")]
        public string ShortNumber { get; set; }

    }

    public class SendSmsMessage : ISmsMessage
    {
        //ddMMyyHHmmss
        [JsonProperty("EXPIRY_DATE")]
        public string ExprityDate { get; set; }

        [JsonProperty("MESSAGE_CLASS")]
        public string MessageClass => "0";

        public string SENDER { get; set; } = "";

        [JsonProperty("SHORT_NUMBER")]
        public string ShortNumber { get; set; }

        //ddMMyyHHmmss
        [JsonProperty("S_DATE")]
        public string SendDate { get; set; }
        

        [JsonProperty("MESSAGE_BODY")]
        public MessageBodies MessageBody { get; set; }

        [JsonProperty("TO_RECEIVERS")]
        public SmsReceivers Receivers { get; set; }
    }

    public class SmsReceivers
    {
        [JsonProperty("msisdn")]
        public List<string> Receivers { get; set; }
    }

    public class MessageBodies
    {
        [JsonProperty("message")]
        public List<string> Messages { get; set; }
    }

    public partial class AddittoryList
    {
        [JsonProperty("ADDITTORY")]
        public AddittoryAddittory[] Addittory { get; set; }
    }
    public partial class AddittoryAddittory
    {
        [JsonProperty("KEY")]
        public string Key { get; set; }

        [JsonProperty("VALUE")]
        public string Value { get; set; }
    }
    public partial class AssetPrice
    {
        [JsonProperty("price")]
        public string Price { get; set; }
    }
    public partial class AssetUnitPrice
    {
        [JsonProperty("price")]
        public string Price { get; set; }
    }


}
