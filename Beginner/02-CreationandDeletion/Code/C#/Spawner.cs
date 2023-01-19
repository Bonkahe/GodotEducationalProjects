using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node
{
	[Export] public Node3D SpawnLocation;
	[Export] public PackedScene NodeToSpawn;

	private List<Node3D> spawnedNodes = new List<Node3D>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("mouse_left_click"))
		{
			Node3D spawnedNode = NodeToSpawn.Instantiate() as Node3D;
			GetTree().Root.AddChild(spawnedNode);

			spawnedNode.GlobalPosition = SpawnLocation.GlobalPosition;
			spawnedNode.GlobalRotation = SpawnLocation.GlobalRotation;

			spawnedNodes.Add(spawnedNode);
        }

		if (inputEvent.IsActionPressed("mouse_right_click"))
		{
			foreach (Node3D node in spawnedNodes)
			{
				node.QueueFree();
            }
			spawnedNodes.Clear();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }
}
