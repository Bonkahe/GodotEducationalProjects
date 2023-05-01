extends Node
class_name DetectionControl

@export
var enemyNavigationControl: EnemyCoreControl;

func OnPlayerEnter(playerContainer:Node3D):
	enemyNavigationControl.TargetBody = playerContainer;

func OnPlayerExit():
	enemyNavigationControl.TargetBody = null;
