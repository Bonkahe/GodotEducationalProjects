extends CharacterBody3D


@export 
var movementSpeed: float = 30
@export
var jumpVelocity: float = 100;
@export
var gravitySpeed: float = 4;

@export
var mouseSensitivity: float = 0.5

var movementInputVector = Vector2(0,0);
var currentYVelocity: float = 0;

func _input(event):
	if event is InputEventMouseMotion:
		rotate(Vector3.UP, deg_to_rad(-event.relative.x * mouseSensitivity));

func _physics_process(delta):
	movementInputVector = Input.get_vector("move_left", "move_right", "move_forward", "move_back")  * movementSpeed;
	currentYVelocity = maxf(currentYVelocity - gravitySpeed, -2000);
	if is_on_floor():
		currentYVelocity = 0;
		if Input.is_action_just_pressed("jump"):
			currentYVelocity = jumpVelocity;
	else:
		currentYVelocity = maxf(currentYVelocity - gravitySpeed, -2000);
	
	velocity = transform.basis * Vector3(movementInputVector.x, currentYVelocity, movementInputVector.y);
	
	move_and_slide();
