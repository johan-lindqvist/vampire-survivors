using Godot;

namespace VampireSurvivors.scripts.ui;

public partial class HealthBar : Control
{
	[Export]
	private ProgressBar progressBar;

	public void SetMaxHealth(int health)
	{
		progressBar.MaxValue = health;
		progressBar.Value = health;
	}

	public void SetHealth(int health)
	{
		progressBar.Value = health;
	}
}