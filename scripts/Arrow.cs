using Godot;
using VampireSurvivors.scripts.components;

namespace VampireSurvivors.scripts;

public partial class Arrow : Area2D
{
	private float damage = 10;

	private float speed = 300f;

	private Vector2 startingPosition;
	
	public override void _Ready()
	{
		Name = "Arrow";
		startingPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Vector2.Right.Rotated(GlobalRotation) * speed * (float)delta;

		if (Position.DistanceTo(startingPosition) > 500)
		{
			QueueFree();
		}
	}
	
	public void OnAreaEntered(Area2D area)
	{
		// Use interface and call "take damage" or something on the enemy
		if (area is not HitboxComponent enemy)
		{
			return;
		}

		if (enemy.IsAlive)
		{
			enemy.Damage(damage);
			QueueFree();
		}
	}
}