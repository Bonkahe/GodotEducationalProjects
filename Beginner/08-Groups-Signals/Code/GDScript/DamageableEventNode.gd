extends DamageableNode

@export
var TransitionType : Tween.TransitionType;

@export
var ScaleSize : Vector3;

@export
var RotationDegrees : float;

@export
var HitPoints : int;

@export
var ContainerNode : Node3D;

signal OnDestruction;
var EndOfGame:bool;

var OriginalPosition;
var OriginalRotation;
var OriginalScale;

var newTween : Tween;


func _ready():
	OriginalPosition = MeshReference.position;
	OriginalRotation = MeshReference.rotation_degrees;
	OriginalScale = MeshReference.scale;

func OnDestroyed():
	EndOfGame = true;
	HitPoints = 0;
	OnHit();

func OnCompleteDestruction():
	ContainerNode.queue_free();

func OnHit():
	if (newTween != null && newTween.is_running()):
		newTween.stop;
	
	HitPoints -= 1;
	
	MeshReference.material_override = HitMaterial;
	
	newTween = create_tween();
	
	newTween.set_trans(TransitionType);
	
	newTween.tween_property(
		MeshReference,
		"position",
		MeshReference.position + Vector3(0,1,0),
		FlashDuration / 2);
	
	var rng = RandomNumberGenerator.new();
	rng.randomize();
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"rotation_degrees",
		MeshReference.rotation_degrees 
			+ Vector3(
				wrapf(rng.randf_range(-RotationDegrees, RotationDegrees), -180, 180),
				wrapf(rng.randf_range(-RotationDegrees, RotationDegrees), -180, 180),
				wrapf(rng.randf_range(-RotationDegrees, RotationDegrees), -180, 180)),
		FlashDuration / 2);
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"scale",
		OriginalScale * ScaleSize,
		FlashDuration / 2);
	
	#Divider --------
	
	newTween.tween_property(
		MeshReference,
		"position",
		OriginalPosition,
		FlashDuration / 2);
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"rotation_degrees",
		OriginalRotation,
		FlashDuration / 2);
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"scale",
		OriginalScale,
		FlashDuration / 2);
	
	
	if (EndOfGame):
		newTween.connect("finished", OnCompleteDestruction.bind());
	else:
		newTween.connect("finished", OnReset.bind());
		if (HitPoints <= 0):
			newTween.connect("finished", OnCompleteDestruction.bind());
			OnDestruction.emit();
		else:
			get_tree().call_group("ScoringGroup", "OnPointsAdded", 1);
	
	newTween.play();

