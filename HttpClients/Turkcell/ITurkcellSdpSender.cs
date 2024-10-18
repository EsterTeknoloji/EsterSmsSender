using Ester.Sms.Abstraction.Cqrs;
using Ester.Sms.Sender.Models;

namespace Ester.Sms.Sender.HttpClients.Turkcell;

public interface ITurkcellSdpSender
{
    Task<ResponseModel?> HandleMessages(ListDynamicSmsTemplateResponse selectedTemplate, string receiver);

}
