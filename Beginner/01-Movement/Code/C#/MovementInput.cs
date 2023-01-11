using Godot;
using System;

public partial class MovementInput : CharacterBody3D
{
	[Export] public float movementSpeed = 30;
    [Export] public float jumpVelocity = 100;
    [Export] public float gravitySpeed = 4;

    [Export] public float mouseSensitivity = 0.5f;

    private Vector2 movementInputVector = new Vector2(0, 0);
    private float currentYVelocity = 0;

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion mouseEvent)
        {
            Rotate(Vector3.Up, Mathf.DegToRad(-mouseEvent.Relative.x * mouseSensitivity));
        }
    }

    public override void _Process(double delta)
	{
        movementInputVector = Input.GetVector("move_left", "move_right", "move_forward", "move_back") * movementSpeed;
        if (IsOnFloor())
        {
            currentYVelocity = 0;
            if (Input.IsActionJustPressed("jump"))
            {
                currentYVelocity = jumpVelocity;
            }
        }
        else
        {
            currentYVelocity = Mathf.Max(currentYVelocity - gravitySpeed, -2000);
        }

        Velocity = Transform.basis * new Vector3(movementInputVector.x, currentYVelocity, movementInputVector.y);

        MoveAndSlide();
    }
}
