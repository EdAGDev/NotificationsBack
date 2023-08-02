using Microsoft.Extensions.Logging;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); // optional (clear providers already added)
    logging.AddFile("Logs/mylog-{Date}.txt");
});

startup.Configure(app, app.Environment);

app.Run();
