using Godot;

namespace VampireSurvivors.scripts.components;

public partial class HealthComponent : Node2D
{
    [Export] public float MaxHealth = 10f;
    [Export] public float Health = 10f;

    public override void _Ready()
    {
        Health = MaxHealth;
    }

    public float Damage(float damage)
    {
        Health -= damage;
        
        return Health;
    }
}
