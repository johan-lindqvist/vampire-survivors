using Godot;

namespace VampireSurvivors.scripts.components;

public partial class EnemyMovementComponent : Node2D
{
    [Export] public float Speed = 100f;
    
    public override void _PhysicsProcess(double delta)
    {
        Position = Position.MoveToward(Player.Instance.Position, (float)delta * Speed);
    }
}
