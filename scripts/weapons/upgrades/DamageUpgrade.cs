using Godot;

namespace VampireSurvivors.scripts.weapons.upgrades;

[GlobalClass]
public partial class DamageUpgrade : BaseUpgrade, IUpgrade
{
	public void ApplyUpgrade(Arrow arrow)
	{
		arrow.Damage *= 2;
	}
}
