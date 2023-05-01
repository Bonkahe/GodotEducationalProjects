extends Node3D
class_name EnemyProjectileControl

@export
var originatingDamageable: DamageableNode;
@export
var moveSpeed: float;
@export
var lifeTime: float;

func _physics_process(delta):
	global_position += -global_transform.basis.z * moveSpeed * delta;
	lifeTime -= delta;
	if (lifeTime <= 0):
		queue_free();
