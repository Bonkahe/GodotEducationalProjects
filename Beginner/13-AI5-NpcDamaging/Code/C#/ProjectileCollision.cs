using Godot;
using System;

public partial class ProjectileCollision : Node
{
    [Export] public EnemyProjectileControl projectileControl { get; set; }
    [Export] public RayCast3D rayCast { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        if (rayCast.IsColliding())
        {
            if ((rayCast.GetCollider() as Node)?.GetNodeOrNull("Damageable") is DamageableNode hitTarget)
            {
                if (hitTarget != projectileControl.OriginatingDamageable)
                {
                    hitTarget.OnHit();
                }
                else
                {
                    //If it hits itself we don't want to queue free
                    return;
                }
            }

            projectileControl.QueueFree();
        }
    }
}
