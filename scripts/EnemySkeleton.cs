using Godot;
using System;

namespace VampireSurvivors.scripts;

public partial class EnemySkeleton : EnemyBase
{
    public EnemySkeleton()
    {
        Health = 200;
        Speed = 50f;
    }
}
