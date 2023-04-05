using Godot;
using System;

public partial class BlasterController : Node
{
	[Export] public RayCast3D RayCastController { get; set; }
	[Export] public PackedScene LaserEffect { get; set; }


	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("mouse_left_click"))
		{
			FireShot();
		}
	}

	private void FireShot()
	{
		Node3D newLaser = LaserEffect.Instantiate() as Node3D;

		AddChild(newLaser);
		newLaser.GlobalPosition = RayCastController.GlobalPosition;
        newLaser.GlobalRotation = RayCastController.GlobalRotation;

		if (RayCastController.IsColliding())
		{
			Vector3 impactPoint = RayCastController.GetCollisionPoint();
			newLaser.Scale = new Vector3(newLaser.Scale.X, newLaser.Scale.Y, impactPoint.DistanceTo(newLaser.GlobalPosition));

			if ((RayCastController.GetCollider() as Node)?.GetNodeOrNull<DamageableNode>("Damageable") is DamageableNode hitTarget)
			{
				hitTarget.OnHit();
			}
		}
		else
		{
            newLaser.Scale = new Vector3(newLaser.Scale.X, newLaser.Scale.Y, RayCastController.TargetPosition.Length());
        }
    }
}
