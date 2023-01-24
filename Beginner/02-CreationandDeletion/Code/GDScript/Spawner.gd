extends Node

@export var SpawnLocation : Node3D;
@export var NodeToSpawn : PackedScene;

var spawnedNodes : Array;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


func  _input(inputEvent):
	if (inputEvent.is_action_pressed("mouse_left_click")):
		var spawnedNode = NodeToSpawn.instantiate() as Node3D;
		get_tree().root.get_child(0).add_child(spawnedNode);
		spawnedNode.global_position = SpawnLocation.global_position;
		spawnedNode.global_rotation = SpawnLocation.global_rotation;
		
		spawnedNodes.append(spawnedNode);
	
	if (inputEvent.is_action_pressed("mouse_right_click")):
		for node in spawnedNodes:
			node.queue_free();
		
		spawnedNodes.clear();

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
