using Godot;
using VampireSurvivors.scripts.components;

namespace VampireSurvivors.scripts.weapons;

public partial class Arrow : Area2D, IDamageAttribute, IStunAttribute
{
	private float speed = 300f;

	private Vector2 startingPosition;

	public float Damage => 1f;

	public float StunDuration => 1f;

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
		if (area is not HitboxComponent hitbox)
		{
			return;
		}

		hitbox.Hit(this, QueueFree);
	}
}

public interface IDamageAttribute
{
	float Damage { get; }
}

public interface IStunAttribute
{
	float StunDuration { get; }
}
