using Godot;

namespace VampireSurvivors.scripts.ui;

public partial class UI : CanvasLayer
{
	[Export] private ProgressBar experienceBar;
	[Export] private Label levelLabel;
	
	public static UI Instance;
	
	public override void _Ready()
	{
		Instance = this;
	}

	public void SetExperienceBar(int currentValue)
	{
		experienceBar.Value = currentValue;
	}

	public void SetExperienceBarMax(int maxValue)
	{
		experienceBar.MaxValue = maxValue;
	}

	public void SetLevelLabel(int value)
	{
		levelLabel.Text = value.ToString();
	}
}
