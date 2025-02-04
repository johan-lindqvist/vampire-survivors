using Godot;

namespace VampireSurvivors.scripts.components;

public partial class HitboxComponent : Area2D
{
    [Export] private HealthComponent healthComponent;

    public float Damage(float damage)
    {
        return healthComponent.Damage(damage);
    }
}
