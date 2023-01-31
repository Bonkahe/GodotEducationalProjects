extends Node3D

@export
var Duration: float;

# Called when the node enters the scene tree for the first time.
func _ready():
	var timer = get_tree().create_timer(Duration);
	timer.connect("timeout", Remove.bind());

func Remove():
	queue_free();
