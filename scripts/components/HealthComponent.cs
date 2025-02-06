using Godot;
using System;

namespace VampireSurvivors.scripts.components;

public partial class HealthComponent : Node2D
{
	[Export] public float MaxHealth = 10f;
	
	[Export] public float Health = 10f;

	public Action OnDeath;

	public override void _Ready()
	{
		Health = MaxHealth;
	}

	public float Damage(float damage)
	{
		if (Health <= 0)
		{
			return 0;
		}
		
		Health = Mathf.Max(Health - damage, 0f);

		if (Health <= 0)
		{
			Die();
		}

		return Health;
	}

	private void Die()
	{
		Health = 0;
		OnDeath?.Invoke();
	}
}
