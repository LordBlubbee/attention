using ATTENTION.Resources.Scripts;
using System;

namespace ATTENTION;

public partial class ExercisePage : ContentPage
{
	int count = 0;
	public ExercisePage()
	{
		//Google: Timer, callback event,
		InitializeComponent();
		updateImage();
        setExerciseNeed();

    }

	private void updateImage()
	{
		int ID = 1;
		if (Pet.pet != null)
		{
			ExercisePageAmount.Text = "Exercise: " + Pet.pet.needCompany;
		}
        ExerciseTex.Source = $"attentionexercise{ID}.png";

    }

	private void pressWalk(object sender, EventArgs e)
	{
		Pet.pet.PlayAudio("sfx_research.ogg");
		Pet.pet.needCompany = Math.Clamp(Pet.pet.needCompany + 30, 0, 150);
		Pet.pet.saveGame();
		updateImage();
    }
    private void pressWorkout(object sender, EventArgs e)
    {
        Pet.pet.PlayAudio("sfx_research2.ogg");
        Pet.pet.needCompany = Math.Clamp(Pet.pet.needCompany + 50, 0, 150);
        Pet.pet.saveGame();
        updateImage();
    }
    private void OnExerciseAdjust(object sender, ValueChangedEventArgs e)
    {
        Pet.pet.exercisePerDay = (float)e.NewValue * 50f;
        setExerciseNeed();
    }
    private void setExerciseNeed()
    {
        if (Pet.pet == null) return;
        exerciseAdjustTex.Text = $"Exercise per day: {(int)Math.Round(Pet.pet.exercisePerDay)}";
        exerciseAdjust.Value = Pet.pet.exercisePerDay / 50f;
    }
}

