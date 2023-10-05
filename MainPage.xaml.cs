using ATTENTION.Resources.Scripts;
using System.ComponentModel;

namespace ATTENTION;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	//Slides, bindings uitzoeken, hier property aanmaken
	//Dan: Geen gedoe met update problemen
	//public string WaterAmount { get; set; } = "Water Default";
    public MainPage()
	{
		BindingContext = this;

        Console.WriteLine("MainPage!");
        Pet Attention = new Pet();
		Pet.pet.mainPage = this;
		//Google: Timer, callback event,
		InitializeComponent();
	}

	public void update()
	{
		//Two hours spent on bindings not working
		//BindableObject.Dispatcher.Dispatch(new Action(() => { }));
		Device.BeginInvokeOnMainThread(() =>
		{
            WaterButton.Text = $"WATER: {Pet.pet.needWater}";
            HealthButton.Text = $"HEALTH: {Pet.pet.needFood}";
            RestButton.Text = $"SLEEP: {Pet.pet.needRest}";
            ExerciseButton.Text = $"EXERCISE: {Pet.pet.needCompany}";
            WorkButton.Text = $"WORK: {Pet.pet.needAttention}";
            MsgDesc.Text = $"{Pet.pet.MostPressingNeed}";
        });
    }

	/*private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count:00} time";
		else
			CounterBtn.Text = $"Clicked {count:00} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

        //Navigation.PushAsync(new NewPage1()); // (Goes to NewPage1.xaml)
    }*/

	private void pressWater(object sender, EventArgs e)
	{
        Navigation.PushAsync(new WaterPage());
    }
    private void pressHealth(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HealthPage());
    }
    private void pressRest(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SleepPage());
    }
    private void pressExercise(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ExercisePage());
    }
    private void pressWork(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HomeworkPage());
    }
}

