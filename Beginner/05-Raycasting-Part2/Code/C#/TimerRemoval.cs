using Godot;
using System;

public partial class TimerRemoval : Node3D
{
	[Export] public float Duration;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SceneTreeTimer timer = GetTree().CreateTimer(Duration);
		timer.Connect("timeout", new Callable(this, "Remove"));
	}

	public void Remove()
	{
		QueueFree();
	}
}
