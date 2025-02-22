using System;
using Godot;
using VampireSurvivors.scripts.weapons;
using VampireSurvivors.scripts.weapons.attributes;

namespace VampireSurvivors.scripts.components;

public partial class HealthComponent : Node2D
{
	[Export]
	public float MaxHealth = 10f;

	[Export]
	public float Health = 10f;

	public Action? OnDeath;

	public override void _Ready()
	{
		Health = MaxHealth;
	}

	public void Damage(IDamageAttribute damageAttribute)
	{
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
