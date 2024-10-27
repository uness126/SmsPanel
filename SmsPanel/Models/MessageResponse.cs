namespace SmsPanel.Models;

public class MessageResponse
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }

    public MessageResponse(string phoneNumber, string message, string status)
    {
        PhoneNumber = phoneNumber;
        Message = message;
        Status = status;
    }
}
