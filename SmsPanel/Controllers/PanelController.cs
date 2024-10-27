using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmsPanel.Models;
using SmsPanel.Services;

namespace SmsPanel.Controllers;

[ApiController]
[Route("[controller]")]
public class PanelController : ControllerBase
{

    private readonly ILogger<PanelController> _logger;
    private readonly ISMSService _smsService;
    private readonly Panel _panelOptions;

    public PanelController(ILogger<PanelController> logger, IOptions<Panel> panelOptions, Func<PanelType, ISMSService> smsService)
    {
        _logger = logger;
        _panelOptions = panelOptions.Value;

        switch (_panelOptions.Type)
        {
            case "GapYar":
                _smsService = smsService(PanelType.GapYar);
                break;
            case "Amoot":
                _smsService = smsService(PanelType.Amoot);
                break;
            default:
                _smsService = smsService(PanelType.GapYar);
                break;
        }
    }

    [HttpGet(Name = "Send")]
    public MessageResponse Send(string phoneNumber, string message)
    {
        var status = _smsService.SendSms(phoneNumber, message);

        _logger.LogInformation($"Panel: {_panelOptions.Type} PhoneNumber: {phoneNumber} Message:{{ {message} }} Result: {status}");

        return new MessageResponse(phoneNumber, message, status);
    }
}