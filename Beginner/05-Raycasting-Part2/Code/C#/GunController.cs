using Godot;
using System;

public partial class GunController : Node
{
	[Export] public RayCast3D RayCast3D { get; set; }
	[Export] public PackedScene LaserEffect { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("mouse_left_click"))
		{
			FireGun();
        }
	}

	private void FireGun()
	{
		Node3D laser = LaserEffect.Instantiate() as Node3D;

		AddChild(laser);
		laser.GlobalPosition = RayCast3D.GlobalPosition;
		laser.GlobalRotation = RayCast3D.GlobalRotation;

		if (RayCast3D.IsColliding())
		{
			Vector3 impactPoint = RayCast3D.GetCollisionPoint();
            laser.Scale = new Vector3(laser.Scale.x, laser.Scale.y, impactPoint.DistanceTo(laser.GlobalPosition));

			if (RayCast3D.GetCollider() is TargetController targetController)
			{
				targetController.OnHit();
			}
        }
		else
		{
			laser.Scale = new Vector3(laser.Scale.x, laser.Scale.y, RayCast3D.TargetPosition.Length());
		}
	}
}
