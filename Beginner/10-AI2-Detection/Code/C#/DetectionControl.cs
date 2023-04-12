using Godot;
using System;

public partial class DetectionControl : Node
{
    [Export] public EnemyNavigationControl enemyNavigationControl { get; set; }

    public void OnPlayerEnter(Node3D playerContainer)
    {
        enemyNavigationControl.TargetBody = playerContainer;
    }

    public void OnPlayerExit()
    {
        enemyNavigationControl.TargetBody = null;
    }
}
