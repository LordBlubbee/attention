using Microsoft.Extensions.Logging;

namespace ATTENTION;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Oswald-Regular.ttf", "OswaldRegular");
				fonts.AddFont("Oswald-Bold.ttf", "OswaldBold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
