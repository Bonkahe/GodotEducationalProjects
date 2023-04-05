using Godot;
using System;

public partial class BlasterControllerv2 : Node
{
	[Export] public AudioStreamPlayer3D BlasterAudio { get; set; }
	[Export] public AudioStream BlasterShotClip { get; set; }
	[Export] public float MinBlasterPitchChange { get; set; }
    [Export] public float MaxBlasterPitchChange { get; set; }

    [Export] public RayCast3D RayCastController { get; set; }
	[Export] public PackedScene LaserEffect { get; set; }

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private float baseBlasterPitch;

	public override void _Ready()
	{
		baseBlasterPitch = BlasterAudio.PitchScale;
	}


	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("mouse_left_click"))
		{
			FireShot();
		}
	}

	private void FireShot()
	{
		rng.Randomize();
		BlasterAudio.PitchScale = baseBlasterPitch + rng.RandfRange(MinBlasterPitchChange, MaxBlasterPitchChange);
		BlasterAudio.Stream = BlasterShotClip;
		BlasterAudio.Play();

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
