extends Node3D

@export
var verticalAngleLimit: float = 45;

@export
var mouseSensitivity: float = 0.5;

var angleLimitInRad: float;

func _ready():
	angleLimitInRad = deg_to_rad(verticalAngleLimit);

func _input(inputEvent):
	if inputEvent is InputEventMouseMotion:
		rotation.x = clampf(rotation.x - deg_to_rad(inputEvent.relative.y * mouseSensitivity), -angleLimitInRad, angleLimitInRad);
