using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class App : Application
{
	public App()
	{
		DependencyService.RegisterSingleton<IDataStore<Pet>>(new CreatureDataStore());
		InitializeComponent();

		MainPage = new AppShell();
	}
}
