using Microsoft.Extensions.Logging;
using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Pages;

namespace TrainingControlPanelDashboard
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton<IDataService, DataService>();
            
            // Register pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<TrainingSessionsPage>();
            builder.Services.AddTransient<AthletesPage>();
            builder.Services.AddTransient<ProgramsPage>();
            builder.Services.AddTransient<AnalyticsPage>();
            builder.Services.AddTransient<SettingsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
