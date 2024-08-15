using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using static Memory_App.Services.SessionService;
using Memory_App.Services;

namespace Memory_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IUserSessionService, SessionService>();
            builder.Services.AddSingleton<AuthenticationServiceFirebase>();
            builder.Services.AddSingleton<AnalyticsService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}