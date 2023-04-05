using Godot;
using System;

public partial class CameraPivot : Node3D
{
	[Export] public float verticalAngleLimit = 45;

	[Export] public float mouseSensitivity = 0.5f;

	private float angleLimitInRad;

	public override void _Ready()
	{

		angleLimitInRad = Mathf.DegToRad(verticalAngleLimit);
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseMotion mouseEvent)
		{
			Rotation = new Vector3(Mathf.Clamp(Rotation.X - Mathf.DegToRad(mouseEvent.Relative.Y * mouseSensitivity), -angleLimitInRad, angleLimitInRad), Rotation.Y, Rotation.Z);
		}
	}
}
