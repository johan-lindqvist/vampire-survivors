using Godot;
using GodotUtilities;
using VampireSurvivors.scripts.player;
using VampireSurvivors.scripts.weapons.upgrades;

namespace VampireSurvivors.scripts.items;

[Scene]
public partial class Upgrade : Area2D
{
	[Node]
	private Label upgradeLabel = null!;

	[Export]
	public BaseUpgrade UpgradeType { get; set; } = null!;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		// upgradeLabel.Text = UpgradeType.UpgradeText;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Player player)
		{
			return;
		}

		player.WeaponUpgrades = player.WeaponUpgrades.Add(UpgradeType);
		QueueFree();
	}
}
