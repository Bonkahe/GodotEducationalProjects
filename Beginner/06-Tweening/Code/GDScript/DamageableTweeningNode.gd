extends DamageableNode

@export
var TransitionType : Tween.TransitionType;

@export
var ScaleSize : Vector3;

@export
var RotationDegrees : float;

var OriginalPosition;
var OriginalRotation;
var OriginalScale;

var newTween : Tween;

func _ready():
	OriginalPosition = MeshReference.global_position;
	OriginalRotation = MeshReference.global_rotation_degrees;
	OriginalScale = MeshReference.scale;

func OnHit():
	if (newTween != null && newTween.is_running()):
		newTween.stop;
	
	MeshReference.material_override = HitMaterial;
	
	newTween = create_tween();
	
	newTween.set_trans(TransitionType);
	
	newTween.tween_property(
		MeshReference,
		"global_position",
		MeshReference.global_position + Vector3(0,1,0),
		FlashDuration / 2);
	
	var rng = RandomNumberGenerator.new();
	rng.randomize();
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"global_rotation_degrees",
		MeshReference.global_rotation_degrees 
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
		"global_position",
		OriginalPosition,
		FlashDuration / 2);
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"global_rotation_degrees",
		OriginalRotation,
		FlashDuration / 2);
	
	newTween.parallel();
	newTween.tween_property(
		MeshReference,
		"scale",
		OriginalScale,
		FlashDuration / 2);
	
	newTween.connect("finished", OnReset.bind());
	
	newTween.play();

