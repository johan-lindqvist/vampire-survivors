using Godot;

namespace VampireSurvivors.scripts.weapons.upgrades;

[GlobalClass]
public partial class StunUpgrade : BaseUpgrade, IUpgrade
{
	public void ApplyUpgrade(Arrow arrow)
	{
		arrow.StunDuration += 1f;
	}
}
