using Godot;

namespace VampireSurvivors.scripts.weapons.upgrades;

[GlobalClass]
public abstract partial class BaseUpgrade : Resource
{
	[Export]
	public string UpgradeText { get; set; } = "N/A";
}
