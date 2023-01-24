using Godot;
using System;

public partial class MouseLock : Node
{
    //Simply locks the mouse within the window.
	public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("ui_cancel"))
        {
            //Toggles between modes when pressing escape.
            Input.MouseMode = (Input.MouseMode == Input.MouseModeEnum.Captured ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured);
        }
    }
}
