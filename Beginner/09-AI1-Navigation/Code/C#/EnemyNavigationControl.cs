using Godot;
using System;

public partial class EnemyNavigationControl : Node3D
{
	[Export] public Node3D TargetBody { get; set; }
	[Export] public float MovementSpeed { get; set; } = 4.0f;
	[Export] public NavigationAgent3D NavigationAgent { get; set; }

	private float MovementDelta;
	private Vector3 LastPosition;

	public override void _Process(double delta)
	{
		if (TargetBody.GlobalPosition != LastPosition)
		{
			LastPosition = TargetBody.GlobalPosition;
			SetTarget(LastPosition);
		}
	}

	private void SetTarget(Vector3 MovementTarget)
	{
		NavigationAgent.TargetPosition = MovementTarget;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (NavigationAgent.IsNavigationFinished())
		{
			return;
		}

		MovementDelta = MovementSpeed * (float)delta;
		Vector3 NextPos = NavigationAgent.GetNextPathPosition();
		Vector3 CurrentPos = GlobalTransform.Origin;
		Vector3 NewVelocity = (NextPos - CurrentPos).Normalized() * MovementDelta;
		NavigationAgent.SetVelocity(NewVelocity);
	}

	public void On_NavigationAgent3DVelocityComputed(Vector3 SafeVelocity)
	{
		Transform3D newTransform = GlobalTransform;
		newTransform.Origin = newTransform.Origin.MoveToward(newTransform.Origin + SafeVelocity, MovementDelta);
		GlobalTransform = newTransform;
	}
}
