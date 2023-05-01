using Godot;
using System;

public partial class EnemyProjectileControl : Node3D
{
    [Export] public DamageableNode OriginatingDamageable { get; set; }
    [Export] public float MoveSpeed { get; set; }
    [Export] public float LifeTime { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += -GlobalTransform.Basis.Z * MoveSpeed * (float)delta;
        LifeTime -= (float)delta;
        if (LifeTime <= 0)
        {
            QueueFree();
        }
    }

}
