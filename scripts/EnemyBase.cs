using Godot;
using System;

namespace VampireSurvivors.scripts;

public abstract partial class EnemyBase : Node2D
{
	protected int Health { get; set; } = 100;
	protected float Speed { get; set; } = 100f;
	
	[Export]
	private Timer timer;
	
	private Player collidingPlayer;
	
	private bool canAttack = true;
	
	public Action<EnemyBase> OnDeath;
	
	private PackedScene itemScene = ResourceLoader.Load<PackedScene>("res://scenes/item.tscn");

	public void TakeDamage(int damage)
	{
		Health -= damage;
	}
	public override void _Process(double delta)
	{
		AttackPlayer();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position = Position.MoveToward(Player.Instance.Position, (float)delta * Speed);
	}

	private void AttackPlayer()
	{
		if (collidingPlayer == null || timer.TimeLeft > 0)
		{
			return;
		}

		if (timer.IsStopped())
		{
			timer.OneShot = true;
			timer.Start();
		}
		
		collidingPlayer.ChangeHealth(-10);
	}

	private void OnBodyEntered(Node2D node)
	{
		if (node is not Player player)
		{
			return;
		}
		
		collidingPlayer = player;
	}

	private void OnBodyExited(Node2D node)
	{
		if (node is not Player)
		{
			return;
		}
		
		collidingPlayer = null;
	}

	public void Die()
	{
		OnDeath?.Invoke(this);
		QueueFree();
		DropItem();
	}

	private void DropItem()
	{
		var item = itemScene.Instantiate<Item>();
		GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, item);
		item.AddToGroup("items");
		item.Position = Position;
	}
}