using Microsoft.Extensions.Options;
using MyAmootSms;
using SmsPanel.Models;

namespace SmsPanel.Services;

public class AmootSmsService : ISMSService
{
    private readonly AmootSmsViewModel _amootSmsOptions;

    public AmootSmsService(IOptions<AmootSmsViewModel> amootSmsOptions)
    {
        _amootSmsOptions = amootSmsOptions.Value;
    }

    public string SendSms(string phoneNumber, string message)
    {
        var amootSms = new AmootSMSWebService2SoapClient(AmootSMSWebService2SoapClient.EndpointConfiguration.AmootSMSWebService2Soap);

        var result = amootSms.SendSimpleAsync(_amootSmsOptions.UserName, _amootSmsOptions.Password, DateTime.Now, message, _amootSmsOptions.SmsNumber, new string[] { phoneNumber }).Result;

        return result.Status.ToString();
    }
}
