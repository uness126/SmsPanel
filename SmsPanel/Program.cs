using SmsPanel;
using SmsPanel.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers();

#region Add Services

DependencyContainer.RegisterServices(builder);

#endregion

#region Add Configure Logging

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddLog4Net("log4net.config");

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILoggerFactory>();
app.ConfigureBuildInExceptionHandler(logger);

app.UseAuthorization();

app.MapControllers();

app.Run();
