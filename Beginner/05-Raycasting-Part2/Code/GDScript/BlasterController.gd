extends Node

@export
var rayCast3D: RayCast3D;
@export
var laserEffect: PackedScene;


func _input(inputEvent):
	if (inputEvent.is_action_pressed("mouse_left_click")):
		FireBlaster();
		
func FireBlaster():
	var newLaser = laserEffect.instantiate() as Node3D;
	
	add_child(newLaser);
	newLaser.global_position = rayCast3D.global_position;
	newLaser.global_rotation = rayCast3D.global_rotation;
	
	if (rayCast3D.is_colliding()):
		var impactPoint = rayCast3D.get_collision_point();
		newLaser.scale = Vector3(newLaser.scale.x, newLaser.scale.y, impactPoint.distance_to(newLaser.global_position));
		var collider = rayCast3D.get_collider();
		if collider is TargetController:
			collider.OnHit();
	else:
		newLaser.scale = Vector3(newLaser.scale.x, newLaser.scale.y, rayCast3D.target_position.length());
