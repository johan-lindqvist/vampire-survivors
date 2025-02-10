using Godot;

namespace VampireSurvivors.scripts.weapons.upgrades;

[GlobalClass]
public partial class SpeedUpgrade : BaseUpgrade, IUpgrade
{
	public void ApplyUpgrade(Arrow arrow)
	{
		arrow.Speed *= 1.25f;
	}
}
