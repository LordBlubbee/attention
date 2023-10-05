using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class HomeworkPage : ContentPage
{
	public HomeworkPage()
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
			HomeworkPageAmount.Text = "Work: " + Pet.pet.needAttention;
		}
        WorkTex.Source = $"attentionwork{ID}.png";

    }

	private void pressWork(object sender, EventArgs e)
	{
		Pet.pet.needAttention = Math.Clamp(Pet.pet.needAttention + 25, 0, 150);
		Pet.pet.saveGame();
		updateImage();
    }
}

