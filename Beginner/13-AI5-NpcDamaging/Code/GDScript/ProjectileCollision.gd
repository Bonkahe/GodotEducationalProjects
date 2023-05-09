extends Node

@export var projectileControl : EnemyProjectileControl;
@export var rayCast : RayCast3D;

func _physics_process(delta):
	if (rayCast.is_colliding()):
		var hitTarget = (rayCast.get_collider() as Node).get_node_or_null("Damageable");
		if (hitTarget != null && hitTarget is DamageableNode):
			if (hitTarget != projectileControl.originatingDamageable):
				hitTarget.OnHit();
			else:
				#If it hits itself we don't want to queue free.
				return;
				
		projectileControl.queue_free();
