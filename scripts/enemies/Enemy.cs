using System;
using Godot;
using VampireSurvivors.scripts.components;
using VampireSurvivors.scripts.weapons;

namespace VampireSurvivors.scripts.enemies;

public partial class Enemy : Node2D
{
	[Export] private HealthComponent healthComponent;

	[Export] private AnimationComponent animationComponent;

	[Export] private HitboxComponent hitboxComponent;

	[Export] private EnemyMovementComponent movementComponent;

	[Export] private ItemDropComponent? itemDropComponent;

	public Action<Enemy>? OnDeath;

	private bool isDead;

	public override void _Ready()
	{
		healthComponent.OnDeath += OnDeathHandler;
		hitboxComponent.OnHit += OnHit;
		animationComponent.PlayAnimation("run");
	}

	private void OnDeathHandler()
	{
		healthComponent.OnDeath -= OnDeathHandler;
		isDead = true;
		OnDeath?.Invoke(this);
		itemDropComponent?.DropItem();
		animationComponent.PlayAnimation("death", QueueFree);
	}

	private void OnHit(Node node, Action? onHitCallback = null)
	{
		if (isDead)
		{
			return;
		}

		if (node is IDamageAttribute damageAttribute)
		{
			healthComponent?.Damage(damageAttribute);
		}

		if (node is IStunAttribute stunAttribute)
		{
			movementComponent?.Stun(stunAttribute);
		}

		onHitCallback?.Invoke();
	}
}
