extends Node3D

@export
var Duration: float;

func _ready():
	var timer = get_tree().create_timer(Duration);
	timer.connect("timeout", Remove.bind());

func Remove():
	queue_free();
