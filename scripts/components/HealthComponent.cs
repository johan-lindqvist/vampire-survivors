using Godot;
using System;
using VampireSurvivors.scripts.weapons;

namespace VampireSurvivors.scripts.components;

public partial class HealthComponent : Node2D
{
	[Export] public float MaxHealth = 10f;

	[Export] public float Health = 10f;

	public Action? OnDeath;

	public override void _Ready()
	{
		Health = MaxHealth;
	}

	public void Damage(IDamageAttribute damageAttribute)
	{
		if (Health <= 0)
		{
			return;
		}

		Health = Mathf.Max(Health - damageAttribute.Damage, 0f);

		if (Health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Health = 0;
		OnDeath?.Invoke();
	}
}
