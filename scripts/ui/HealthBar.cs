using Godot;
using GodotUtilities;

namespace VampireSurvivors.scripts.ui;

[Scene]
public partial class HealthBar : Control
{
	[Node]
	private ProgressBar progressBar = null!;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

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
