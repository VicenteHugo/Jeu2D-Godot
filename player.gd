extends CharacterBody2D


const SPEED = 300.0
@onready var animSprite = $AnimatedSprite2D
var right = true

func _inputHandler():
	velocity = Vector2.ZERO
	
	if (Input.is_action_pressed("RIGHT_AXIS")):
		velocity.x += SPEED
		if (!right):
			flip()
	elif (Input.is_action_pressed("LEFT_AXIS")):
		velocity.x -= SPEED
		if (right):
			flip()

	if (Input.is_action_pressed("DOWN_AXIS")):
		velocity.y += SPEED
	elif (Input.is_action_pressed("UP_AXIS")):
		velocity.y -= SPEED
		
	if (velocity.length() > 0):
		animSprite.play("run")
		velocity = velocity.normalized() * SPEED
	else:
		animSprite.play("idle")

func _physics_process(delta: float) -> void:
	_inputHandler()
	move_and_slide()
	
func flip():
	scale.x*= -1
	right = !right
	
	
func Save():
	var save_dict = {
		"filename" : get_scene_file_path(),
		"parent" : get_parent().get_path(),
		"PosX" : position.x,
		"PosY" : position.y
	}
	return save_dict
