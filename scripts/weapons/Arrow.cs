using Godot;
using VampireSurvivors.scripts.components;
using VampireSurvivors.scripts.weapons.attributes;

namespace VampireSurvivors.scripts.weapons;

public partial class Arrow : Area2D, IDamageAttribute, IStunAttribute
{
	public float Speed { get; set; } = 300f;

	private Vector2 startingPosition;

	public float Damage { get; set; } = 10f;

	public float StunDuration { get; set; } = 0f;

	public override void _Ready()
	{
		Name = "Arrow";
		startingPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Vector2.Right.Rotated(GlobalRotation) * Speed * (float)delta;

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
