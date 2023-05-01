using Godot;
using System;

public partial class EnemyCoreControl : Node3D
{
	[Export] public Node3D TargetBody { get; set; }
	[Export] public float MovementSpeed { get; set; } = 4.0f;
	[Export] public NavigationAgent3D NavigationAgent { get; set; }

	[Export] public float WanderUpdateCooldown { get; set; } = 2;
	[Export] public float WanderRange { get; set; } = 15;

	private float MovementDelta;
	private Vector3 LastPosition;

	private enum AIStates { Idle, Wandering, Chasing}
	private AIStates CurrentState = AIStates.Idle;

	private float ChangeCooldown;

	public override void _Process(double delta)
	{
		UpdateState();

		switch (CurrentState)
		{
			case AIStates.Idle:
				ChangeCooldown -= (float)delta;
				if (ChangeCooldown <= 0)
				{
					ChangeCooldown = WanderUpdateCooldown;
					CurrentState = AIStates.Wandering;

					SetTarget(GetWanderPosition());
				}
				break;
			case AIStates.Wandering:
                ChangeCooldown -= (float)delta;
                if (ChangeCooldown <= 0)
                {
                    ChangeCooldown = WanderUpdateCooldown;
                    CurrentState = AIStates.Idle;
                }
                break;
			case AIStates.Chasing:
                if (TargetBody != null && TargetBody.GlobalPosition != LastPosition)
                {
                    LastPosition = TargetBody.GlobalPosition;
                    SetTarget(LastPosition);
                }
                break;
		}
	}

	private void UpdateState()
	{
		if (TargetBody != null)
		{
			CurrentState = AIStates.Chasing;
			return;
		}
		if (CurrentState == AIStates.Chasing)
		{
			CurrentState = AIStates.Idle;
		}
	}

	private Vector3 GetWanderPosition()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		rng.Randomize();
		return GlobalPosition + new Vector3(rng.RandfRange(-WanderRange, WanderRange), 0, rng.RandfRange(-WanderRange, WanderRange));
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
