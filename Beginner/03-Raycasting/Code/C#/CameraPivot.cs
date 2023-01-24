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
			Rotation = new Vector3(Mathf.Clamp(Rotation.x - Mathf.DegToRad(mouseEvent.Relative.y * mouseSensitivity), -angleLimitInRad, angleLimitInRad), Rotation.y, Rotation.z);
		}
	}
}
