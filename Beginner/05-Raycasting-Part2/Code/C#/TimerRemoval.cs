using Godot;
using System;

public partial class TimerRemoval : Node3D
{
	[Export] public float Duration;

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
