using System;
using Godot;

namespace VampireSurvivors.scripts;

public partial class Enemy : Area2D
{
	[Export]
	public float Speed = 100;

	[Export]
	public Timer Timer;
	
	private Player collidingPlayer;
	
	private bool canAttack = true;
	
	public Action<Enemy> OnDeath;
	
	private PackedScene itemScene = ResourceLoader.Load<PackedScene>("res://scenes/item.tscn");

	public override void _Process(double delta)
	{
		AttackPlayer();
	}

	public override void _PhysicsProcess(double delta) => Position = Position.MoveToward(Player.Instance.Position, (float)delta * Speed);

	private void AttackPlayer()
	{
		if (collidingPlayer == null || Timer.TimeLeft > 0)
		{
			return;
		}

		if (Timer.IsStopped())
		{
			Timer.OneShot = true;
			Timer.Start();
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
