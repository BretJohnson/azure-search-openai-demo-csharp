namespace MauiBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient<ApiClient>(client =>
        {
            // Configure this to point to your deployed API. For example, https://MY_HOSTED_APP.example.azurecontainerapps.io/
            // If you want to use the Azure hosted web app, then the variables will automatically be set.
            // If you are using a local web app, you will need to replace the URI here with your local host or dev tunnel.
            client.BaseAddress = new Uri(AppSettings.ServiceWebUri);
        });
        builder.Services.AddScoped<OpenAIPromptQueue>();
        builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
        builder.Services.AddSingleton<ISessionStorageService, SessionStorageService>();
        builder.Services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
        builder.Services.AddSingleton<ISpeechSynthesisService, SpeechSynthesisService>();
        builder.Services.AddMudServices();

        return builder.Build();
	}
}
