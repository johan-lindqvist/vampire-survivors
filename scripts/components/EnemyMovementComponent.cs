using Godot;

namespace VampireSurvivors.scripts.components;

public partial class EnemyMovementComponent : Node2D
{
	[Export] public float Speed = 100f;

	[Export]
	private Node2D enemy;

	public override void _PhysicsProcess(double delta)
	{
		enemy.Position = enemy.Position.MoveToward(Player.Instance.Position, (float)delta * Speed);
	}
}
