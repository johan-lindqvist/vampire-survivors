using Godot;
using GodotUtilities;
using VampireSurvivors.scripts.items;

namespace VampireSurvivors.scripts.enemies;

public partial class UpgradeDropComponent : Node2D
{
	[Export]
	private PackedScene upgradeScene = null!;

	public void Drop()
	{
		var upgrade = upgradeScene.Instantiate<Upgrade>();
		upgrade.GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, upgrade);
		upgrade.AddToGroup("upgrades");
		upgrade.Position = Vector2.Zero;
	}
}
