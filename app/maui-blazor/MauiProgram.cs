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
            // TODO: Configure this to point to your deployed API. For example, https://MY_HOSTED_APP.example.azurecontainerapps.io/
            client.BaseAddress = new Uri("TODO");
        });
        builder.Services.AddScoped<OpenAIPromptQueue>();

        builder.Services.AddSingleton<ILocalStorageServiceWrapper, MauiLocalStorageService>();
        builder.Services.AddSingleton<ISessionStorageServiceWrapper, MauiSessionStorageService>();
        builder.Services.AddSingleton<ISpeechRecognitionServiceWrapper, MauiSpeechRecognitionService>();
        builder.Services.AddSingleton<ISpeechSynthesisServiceWrapper, MauiSpeechSynthesisService>();
        builder.Services.AddSingleton<ISpeechSynthesisServiceExtensions, MauiSpeechSynthesisServiceExtensions>();

        builder.Services.AddMudServices();

        return builder.Build();
	}
}
