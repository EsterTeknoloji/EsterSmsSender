using Azure.Core;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Serilog;
using Core.Security.CipherHelpers;
using Ester.Sms.Abstraction.Cqrs;
using Ester.Sms.Abstraction.Cqrs.Client.GetByVariant;
using Ester.Sms.Sender.Constants;
using Ester.Sms.Sender.Models;
using Ester.Sms.Sender.Models.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using ThirdParty.Json.LitJson;

namespace Ester.Sms.Sender.HttpClients.Turkcell;

public class TurkcellSdpSender : ITurkcellSdpSender
{
    private HttpClient _httpClient;
    private TurkcellSdpSettings _turkcellSdpSettings;
    private LoggerServiceBase _loggerServiceBase;
    private CipherHelper _cipherHelper;
    private IMediator _mediator;
    private IConfiguration _configuration;
    public TurkcellSdpSender(HttpClient httpClient, TurkcellSdpSettings turkcellSdpSettings, LoggerServiceBase loggerServiceBase, IMediator mediator, CipherHelper cipherHelper, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _turkcellSdpSettings = turkcellSdpSettings;
        _loggerServiceBase = loggerServiceBase;
        _mediator = mediator;
        _cipherHelper = cipherHelper;
        _configuration = configuration;
    }

    public async Task<ResponseModel?> HandleMessages(ListDynamicSmsTemplateResponse selectedTemplate, string receiver)
    {

        var client =  await SetSession(selectedTemplate.Variant);
        if (selectedTemplate?.Cost > 0)
        {
            //var chargeRequest = new ChargeRequest
            //{
            //    MSISDN = receiver,
            //    ProductCategory = "External",
            //    RequestTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"),
            //    Units = "1",
            //    AssetBaseUnitPrice = selectedTemplate.Cost,
            //    AssetPrice = new AssetPrice
            //    {
            //        Price = selectedTemplate.Cost.ToString(CultureInfo.InvariantCulture),
            //    },
            //    AssetUnitPrice = new AssetUnitPrice
            //    {
            //        Price = selectedTemplate.Cost.ToString(CultureInfo.InvariantCulture),
            //    }
            //};
            //var doCharge = await DoCharge(chargeRequest);



            var sendChargedSms = new SendChargedSmsMessage
            {
                ProductCategory = "External",
                AssetUnits = 1,
                AssetId = "",
                RequestTime = DateTime.Now.ToString("yyyymmddHHmmss"),
                AccessMethod = "WEB",
                ShortNumber = client.ShortNumber,
                MtShortNumber = client.ShortNumber,
                PayerMsisdn = receiver,
                ReceiverMsisdn = receiver,
                AssetBasePrice = selectedTemplate.Cost.ToString(CultureInfo.InvariantCulture),
                AssetUnitPrice = new AssetUnitPrice { Price = selectedTemplate.Cost.ToString(CultureInfo.InvariantCulture) },
                MessageBody = new MessageBodies
                {
                    Messages = new List<string> { selectedTemplate.Template }
                },
            };
            return await SendChargedSms(sendChargedSms);
        }


        var sendSms = new SendSmsMessage
        {
            ShortNumber = client.ShortNumber,
            Receivers = new SmsReceivers
            {
                Receivers = new List<string> { receiver }
            },
            MessageBody = new MessageBodies
            {
                Messages = new List<string> { selectedTemplate.Template }
            },
            ExprityDate = DateTime.Now.AddDays(7).ToString("ddMMyyHHmmss"),
            SendDate = DateTime.Now.ToString("ddMMyyHHmmss")            
        };

        return await SendSms(sendSms);

    }



    private async Task<TSOresponse?> DoCharge(ChargeRequest request)
    {
        string jsonData = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(_turkcellSdpSettings.ChargeRequest, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<TSOresponse>(await response.Content.ReadAsStringAsync());
            _loggerServiceBase.Info($"Do Charge : {jsonData}");
            return responseData;
        }
        _loggerServiceBase.Fatal($"Do Charge Error : {jsonData}");
        _loggerServiceBase.Fatal($"Do Charge Error : {response.StatusCode} : {response.ReasonPhrase}");
        return null;
    }

    private async Task<ResponseModel?> SendSms(SendSmsMessage message)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(message);
            _loggerServiceBase.Info($"Waiting Send Sms : {jsonData}");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_turkcellSdpSettings.SendSms, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                _loggerServiceBase.Info($"Send Sms : {jsonData}");
                return responseData;
            }
            _loggerServiceBase.Fatal($"Send Sms Error : {jsonData}");
            _loggerServiceBase.Fatal($"Send Sms Error : {response.StatusCode} : {response.ReasonPhrase}");

            return null;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.Fatal($"Send Sms Error : {ex.Message}");
            return null;
        }
    }

    private async Task<ResponseModel?> SendChargedSms(SendChargedSmsMessage message)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(message);
            _loggerServiceBase.Info($"Waiting Send Charged Sms : {jsonData}");
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_turkcellSdpSettings.SendChargedSms, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                _loggerServiceBase.Info($"Do Charged Sms : {jsonData}");
                return responseData;
            }

            _loggerServiceBase.Fatal($"Send Charged Sms Error : {response.StatusCode} : {response.ReasonPhrase}");
            return null;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.Fatal($"Send Sms Error : {ex.Message}");
            return null;

        }
    }

    private async Task<GetByVariantClientResponse> SetSession(string variant)
    {
        var client = await _mediator.Send(new GetByVariantClientQuery { Variant = variant });
        var secret = _configuration.GetValue<string>("SecurityKey");

        var uName = _cipherHelper.Decrypt(client.UserName, secret);
        var pass = _cipherHelper.Decrypt(client.Password, secret);

        string jsonData = JsonConvert.SerializeObject(new AuthRequest
        {
            VariantId = variant,
            Password = client.Password,
            UserName = client.UserName,
        });
        _loggerServiceBase.Info($"TokenRequest : {jsonData}");

        jsonData = JsonConvert.SerializeObject(new AuthRequest
        {
            VariantId = variant,
            Password = pass,
            UserName = uName,
        });


        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(_turkcellSdpSettings.SessionRequest, content);
        
        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            if (!string.IsNullOrEmpty(responseData.SessionId))
            {
                _loggerServiceBase.Info($"TokenRequest Success: {responseData.SessionId}");
                _httpClient.DefaultRequestHeaders.Remove("token");
                _httpClient.DefaultRequestHeaders.Add("token", responseData.SessionId);
                return client;
            }
            _loggerServiceBase.Fatal($"Session Error : {jsonData}");
            _loggerServiceBase.Fatal($"Session Error : {response.StatusCode} : {response.ReasonPhrase}");
        }
        _loggerServiceBase.Error($"TokenRequest Error: {jsonData}");

        throw new BusinessException($"Session Error : {jsonData}");
    }


}
