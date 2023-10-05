using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class HealthPage : ContentPage
{
	public HealthPage()
	{
		//Google: Timer, callback event,
		InitializeComponent();
		updateImage();
	}

	private void updateImage()
	{
		int ID = 3;
		if (Pet.pet != null)
		{
			HealthPageAmount.Text = "Health: " + Pet.pet.needFood;
            switch (Pet.pet.needFood)
            {
                case > 80:
                    ID = 3;
                    break;
                case > 35:
                    ID = 2;
                    break;
                default:
                    ID = 1;
                    break;
            }
        }
        HealthTex.Source = $"attentionhealth{ID}.png";

    }

	private void pressHealthSmall(object sender, EventArgs e)
	{
		Pet.pet.needFood = Math.Clamp(Pet.pet.needFood + 25, 0, 150);
		Pet.pet.saveGame();
		updateImage();
    }
    private void pressHealthLarge(object sender, EventArgs e)
    {
        Pet.pet.needFood = Math.Clamp(Pet.pet.needFood + 50, 0, 150);
        Pet.pet.saveGame();
        updateImage();
    }
}

