using System;
using Godot;
using GodotUtilities;
using VampireSurvivors.scripts.components;
using VampireSurvivors.scripts.weapons;
using VampireSurvivors.scripts.weapons.attributes;

namespace VampireSurvivors.scripts.enemies;

[Scene]
public partial class Enemy : Node2D
{
	[Node]
	private HealthComponent healthComponent = null!;

	[Node]
	private AnimationComponent animationComponent = null!;

	[Node]
	private HitboxComponent hitboxComponent = null!;

	[Node]
	private EnemyMovementComponent enemyMovementComponent = null!;

	[Node]
	private ItemDropComponent itemDropComponent = null!;

	[Node]
	private UpgradeDropComponent upgradeDropComponent = null!;

	public Action<Enemy>? OnDeath;

	private bool isDead;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		healthComponent.OnDeath += OnDeathHandler;
		hitboxComponent.OnHit += OnHit;
		animationComponent.PlayAnimation("run");
	}

	private void OnDeathHandler()
	{
		healthComponent.OnDeath -= OnDeathHandler;
		hitboxComponent.OnHit -= OnHit;
		isDead = true;
		OnDeath?.Invoke(this);
		itemDropComponent?.DropItem();
		upgradeDropComponent.Drop();
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
			enemyMovementComponent?.Stun(stunAttribute);
		}

		onHitCallback?.Invoke();
	}
}
