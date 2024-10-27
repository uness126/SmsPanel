using Microsoft.Extensions.Options;
using SmsPanel.Models;
using System.Net;
using System.Text;

namespace SmsPanel.Services;

public class GapYarSmsService : ISMSService
{
    private readonly GapYarSmsViewModel _gapYarSmsOptions;
    public GapYarSmsService(IOptions<GapYarSmsViewModel> gapYarSmsOptions)
    {
        _gapYarSmsOptions = gapYarSmsOptions.Value;
    }

    public string SendSms(string phoneNumber, string message)
    {
        var request = WebRequest.Create("http://ippanel.com/services.jspd");

        request.Method = "POST";
        string postData = $"op=send&uname={_gapYarSmsOptions.UserName}&pass={_gapYarSmsOptions.Password}&message={message}&to={phoneNumber}&from={_gapYarSmsOptions.SmsNumber}";

        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;
        
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        
        WebResponse response = request.GetResponse();

        var resultDesc = ((HttpWebResponse)response).StatusDescription.ToString();
        
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();

        reader.Close();
        dataStream.Close();
        response.Close();

        return responseFromServer;
    }
}
