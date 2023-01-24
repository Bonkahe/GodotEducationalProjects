extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED);

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		match Input.get_mouse_mode():
			Input.MOUSE_MODE_CAPTURED:
				Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
			Input.MOUSE_MODE_VISIBLE:
				Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		
