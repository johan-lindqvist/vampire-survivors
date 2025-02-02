using Godot;
using System;

public partial class Ui : Control
{
	[Export] private ProgressBar experienceBar;
	[Export] private Label levelLabel;
	
	public static Ui Instance;
	
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
