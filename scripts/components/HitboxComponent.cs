using Godot;
using VampireSurvivors.scripts.enemies;
using VampireSurvivors.scripts.weapons;

namespace VampireSurvivors.scripts.components;

public partial class HitboxComponent : Area2D
{
	[Export] private HealthComponent? healthComponent;

	[Export] private EnemyMovementComponent? movementComponent;

	public void Hit(Node hittingNode)
	{
		if (hittingNode is IDamageAttribute damageAttribute)
		{
			healthComponent?.Damage(damageAttribute);
		}

		if (hittingNode is IStunAttribute stunAttribute)
		{
			movementComponent?.Stun(stunAttribute);
		}
	}
}
