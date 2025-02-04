using System;
using Godot;
using VampireSurvivors.scripts.components;

namespace VampireSurvivors.scripts;

public partial class SkeletonEnemy : Node2D
{
	[Export] private HealthComponent healthComponent;

	public Action<SkeletonEnemy> OnDeath;
	
	public override void _Ready()
	{
		healthComponent.OnDeath += OnDeathHandler;
	}

	public void OnDeathHandler()
	{
		OnDeath?.Invoke(this);
		QueueFree();
	}
}
