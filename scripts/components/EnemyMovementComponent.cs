using Godot;

namespace VampireSurvivors.scripts.components;

public partial class EnemyMovementComponent : Node2D
{
	[Export] public float Speed = 100f;

	[Export] private Enemy enemy;

	private bool canMove = true;

	public override void _Ready()
	{
		enemy.OnDeath += OnDeath;
	}

	public override void _Process(double delta)
	{
		if (!canMove)
		{
			return;
		}
		
		enemy.Position = enemy.Position.MoveToward(Player.Instance.Position, (float)delta * Speed);
	}

	private void OnDeath(Enemy e)
	{
		canMove = false;
	}
}
