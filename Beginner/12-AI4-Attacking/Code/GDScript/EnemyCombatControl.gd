extends Node


@export
var enemyCoreControl: EnemyCoreControl;


@export
var thisDamageable: Node;

@export
var projectileScene: PackedScene;

@export
var firingCooldown: float;

var currentCooldown: float;

func _ready():
	currentCooldown = firingCooldown;

func _physics_process(delta):
	if (enemyCoreControl.TargetBody != null):
		currentCooldown -= delta;
		if (currentCooldown <= 0):
			currentCooldown += firingCooldown;
			var projectile = projectileScene.instantiate() as EnemyProjectileControl;
			add_child(projectile);
			projectile.global_position = enemyCoreControl.global_position + Vector3(0,1,0);
			projectile.look_at(enemyCoreControl.TargetBody.global_position + Vector3(0,1,0));
			projectile.originatingDamageable = thisDamageable as DamageableNode;
