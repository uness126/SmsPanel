using SmsPanel.Models;
using SmsPanel.Services;

namespace SmsPanel;

public static class DependencyContainer
{
    public static void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<GapYarSmsService>();
        builder.Services.AddScoped<AmootSmsService>();

        builder.Services.AddScoped<Func<PanelType, ISMSService>>(serviceProvider => key =>
        {
            switch (key)
            {
                case PanelType.GapYar:
                    return serviceProvider.GetService<GapYarSmsService>();
                case PanelType.Amoot:
                    return serviceProvider.GetService<AmootSmsService>();
                default:
                    return serviceProvider.GetService<GapYarSmsService>();
            }
        });

        builder.Services.Configure<GapYarSmsViewModel>(
            builder.Configuration.GetSection(GapYarSmsViewModel.GapYarSms));

        builder.Services.Configure<AmootSmsViewModel>(
            builder.Configuration.GetSection(AmootSmsViewModel.AmootSms));

        builder.Services.Configure<Panel>(builder.Configuration.GetSection("PanelType"));
    }
}
