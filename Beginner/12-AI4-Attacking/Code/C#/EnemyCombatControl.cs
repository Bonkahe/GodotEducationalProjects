using Godot;
using System;

public partial class EnemyCombatControl : Node
{
    [Export] public EnemyCoreControl EnemyCoreControl { get; set; }
    [Export] public Node ThisDamageable { get; set; }
    [Export] public PackedScene ProjectileScene { get; set; }

    [Export] public float FiringCooldown { get; set; }

    private float currentCooldown;

    public override void _Ready()
    {
        currentCooldown = FiringCooldown;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (EnemyCoreControl.TargetBody != null)
        {
            currentCooldown -= (float)delta;
            if (currentCooldown <= 0)
            {
                currentCooldown += FiringCooldown;
                EnemyProjectileControl projectile = ProjectileScene.Instantiate() as EnemyProjectileControl;
                AddChild(projectile);
                projectile.GlobalPosition = EnemyCoreControl.GlobalPosition + new Vector3(0, 1, 0);
                projectile.LookAt(EnemyCoreControl.TargetBody.GlobalPosition + new Vector3(0, 1, 0));
                projectile.OriginatingDamageable = ThisDamageable as DamageableNode;
            }
        }
    }
}
