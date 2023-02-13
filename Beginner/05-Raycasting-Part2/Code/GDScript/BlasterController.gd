extends Node


@export
var rayCastController: RayCast3D;

@export
var laserEffect: PackedScene;

func _input(inputEvent):
	if (inputEvent.is_action_pressed("mouse_left_click")):
		FireShot();
		
func FireShot():
	var newLaser = laserEffect.instantiate() as Node3D;
	
	add_child(newLaser);
	newLaser.global_position = rayCastController.global_position;
	newLaser.global_rotation = rayCastController.global_rotation;
	
	if (rayCastController.is_colliding()):
		var impactPoint = rayCastController.get_collision_point();
		newLaser.scale = Vector3(newLaser.scale.x, newLaser.scale.y, impactPoint.distance_to(newLaser.global_position));
		var collider = (rayCastController.get_collider() as Node).get_node_or_null("Damageable");
		if (collider != null && collider is DamageableNode):
			collider.OnHit();
	else:
		newLaser.scale = Vector3(newLaser.scale.x, newLaser.scale.y, rayCastController.target_position.length());
