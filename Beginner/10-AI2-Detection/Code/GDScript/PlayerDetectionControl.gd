extends Area3D

@export
var playerContainer: Node3D;

func OnAreaEnter(body:Node3D):
	var aiDetection = body.get_node_or_null("Detection");
	if (aiDetection != null && aiDetection is DetectionControl):
		aiDetection.OnPlayerEnter(playerContainer);

func OnAreaExit(body:Node3D):
	var aiDetection = body.get_node_or_null("Detection");
	if (aiDetection != null && aiDetection is DetectionControl):
		aiDetection.OnPlayerExit();
