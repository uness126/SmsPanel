namespace SmsPanel.Services;

public interface ISMSService
{
    string SendSms(string phoneNumber, string message);
}
