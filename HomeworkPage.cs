using ATTENTION.Resources.Scripts;

namespace ATTENTION;

public partial class HomeworkPage : ContentPage
{
	public HomeworkPage()
	{
		//Google: Timer, callback event,
		InitializeComponent();
		updateImage();
        setWorkNeed();
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
        Pet.pet.PlayAudio("sfx_research.ogg");
        Pet.pet.needAttention = Math.Clamp(Pet.pet.needAttention + 25, 0, 150);
		Pet.pet.saveGame();
		updateImage();
    }
    private void OnWorkAdjust(object sender, ValueChangedEventArgs e)
    {
        Pet.pet.workPerDay = (float)e.NewValue * 50f;
        setWorkNeed();
    }
    private void setWorkNeed()
    {
        if (Pet.pet == null) return;
        workAdjustTex.Text = $"Work per day: {(int)Math.Round(Pet.pet.workPerDay)}";
        workAdjust.Value = Pet.pet.workPerDay / 50f;
    }
}

