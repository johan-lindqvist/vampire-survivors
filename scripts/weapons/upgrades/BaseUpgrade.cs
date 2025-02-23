using Godot;

namespace VampireSurvivors.scripts.weapons.upgrades;

[GlobalClass]
public abstract partial class BaseUpgrade : Resource
{
	[Export]
	public string Name { get; set; } = "N/A";

	[Export]
	public string Description { get; set; } = "N/A";
}
