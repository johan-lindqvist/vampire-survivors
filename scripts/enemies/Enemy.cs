using System;
using Godot;
using VampireSurvivors.scripts.components;

namespace VampireSurvivors.scripts.enemies;

public partial class Enemy : Node2D
{
	[Export] private HealthComponent healthComponent;

	[Export] private AnimationComponent animationComponent;

	[Export] private HitboxComponent hitboxComponent;

	public Action<Enemy> OnDeath;

	public override void _Ready()
	{
		healthComponent.OnDeath += OnDeathHandler;
		animationComponent.PlayAnimation("run");
	}

	private void OnDeathHandler()
	{
		OnDeath?.Invoke(this);
		healthComponent.OnDeath -= OnDeathHandler;
		animationComponent.PlayAnimation("death", QueueFree);
	}
}
