extends Node
class_name DamageableNode

@export
var MeshReference: MeshInstance3D;
@export
var BaseMaterial: Material;
@export
var HitMaterial: Material;

@export
var FlashDuration: float;

func OnHit():
	MeshReference.material_override = HitMaterial;
	var timer = get_tree().create_timer(FlashDuration);
	timer.connect("timeout", OnReset.bind());
	
func OnReset():
	MeshReference.material_override = BaseMaterial;
