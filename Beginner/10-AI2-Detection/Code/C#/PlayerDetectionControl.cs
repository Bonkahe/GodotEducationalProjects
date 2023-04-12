using Godot;
using System;

public partial class PlayerDetectionControl : Node
{
    [Export] public Node3D PlayerContainer { get; set; }

    public void OnAreaEnter(Node3D body)
    {
        if (body.GetNodeOrNull<DetectionControl>("Detection") is DetectionControl detectedNPC)
        {
            detectedNPC.OnPlayerEnter(PlayerContainer);
        }
    }

    public void OnAreaExit(Node3D body)
    {
        if (body.GetNodeOrNull<DetectionControl>("Detection") is DetectionControl detectedNPC)
        {
            detectedNPC.OnPlayerExit();
        }
    }
}
