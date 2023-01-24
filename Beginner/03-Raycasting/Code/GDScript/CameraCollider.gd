extends Node3D

@export
var cameraNode: Node3D;

@export
var rayCast3D: RayCast3D;

@export
var standOffDistance: float = 0.1;
@export_flags_3d_physics
var CameraColliderLayers: int;

var cameraLocalStartingPosition: Vector3;

# Called when the node enters the scene tree for the first time.
func _ready():
	cameraLocalStartingPosition = to_local(cameraNode.global_position);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
#	if (rayCast3D.is_colliding()):
#		cameraNode.position = Vector3(cameraNode.position.x, cameraNode.position.y, rayCast3D.get_collision_point().distance_to(rayCast3D.global_position) - standOffDistance);
#	else:
#		cameraNode.global_position = rayCast3D.to_global(rayCast3D.target_position);
#
	RayCast();

func RayCast():
	var query = PhysicsRayQueryParameters3D.new();
	
	query.from = global_position;
	query.to = to_global(cameraLocalStartingPosition);
	query.collide_with_areas = false;
	query.collide_with_bodies = true;
	query.collision_mask = CameraColliderLayers;
	
	var hitDictionary = get_world_3d().direct_space_state.intersect_ray(query);
	if (hitDictionary):
		cameraNode.position = Vector3(cameraNode.position.x, cameraNode.position.y, hitDictionary["position"].distance_to(global_position) - standOffDistance);
	else:
		cameraNode.global_position = to_global(cameraLocalStartingPosition);
	
