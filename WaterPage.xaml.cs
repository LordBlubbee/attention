using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class WaterPage : ContentPage
{
	int count = 0;
	public WaterPage()
	{
		//Google: Timer, callback event,
		InitializeComponent();
		updateImage();
	}

	private void updateImage()
	{
		int ID = 4;
		if (Pet.pet != null)
		{
			WaterPageAmount.Text = "Water: " + Pet.pet.needWater;
			switch (Pet.pet.needWater)
			{
				case > 75:
					ID = 4;
					break;
                case > 50:
					ID = 3;
                    break;
                case > 25:
					ID = 2;
                    break;
				default:
					ID = 1;
                    break;
            }
		}
        DrinkTex.Source = $"attentionwater{ID}.png";

    }

	private void pressMainWater(object sender, EventArgs e)
	{
		Pet.pet.needWater = Math.Clamp(Pet.pet.needWater+10, 0, 100);
		Pet.pet.saveGame();
        Console.WriteLine("Pressing water: " + Pet.pet.needWater);
		updateImage();
    }
}

