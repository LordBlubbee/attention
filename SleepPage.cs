using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class SleepPage : ContentPage
{
	public SleepPage()
	{
		//Google: Timer, callback event,
		InitializeComponent();
		updateImage();
	}

	private void updateImage()
	{
		int ID = 1;
		if (Pet.pet != null)
		{
			SleepPageAmount.Text = "Sleep: " + Pet.pet.needRest;
		}
        SleepTex.Source = $"attentionsleep{ID}.png";

    }

	private void pressSleep(object sender, EventArgs e)
	{
		Pet.pet.needRest = Math.Clamp(Pet.pet.needRest + 80, 0, 110);
		Pet.pet.saveGame();
		updateImage();
    }
}

