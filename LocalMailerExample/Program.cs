// See https://aka.ms/new-console-template for more information
using LocalMailer;
using LocalMailerExample;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = Host.CreateDefaultBuilder(args);

var app = builder.ConfigureDefaults(Array.Empty<string>()).
    ConfigureAppConfiguration(builder =>
    {
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices(services =>
    {
        var mailSettings = new MailSettings();
        configuration.GetSection(nameof(MailSettings)).Bind(mailSettings);
        services.AddSingleton(mailSettings);
        services.AddScoped<LocalEmailer>();
    })
    .Build();

var emailer = app.Services.GetRequiredService<LocalEmailer>();

await emailer.SendMailAsync(new ExampleMailMessage());
