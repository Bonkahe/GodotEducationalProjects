extends Node

@export
var ScoreLabel:Label;

@export
var VictoryLabel:Label;

var CurrentPoints:int = 0;

var HasWon:bool;

func OnPointsAdded(points:int):
	CurrentPoints += points;
	ScoreLabel.text = "Score:" + str(CurrentPoints);
	
func OnVictory():
	if (HasWon):
		return;
	
	HasWon = true;
	get_tree().call_group("DamageableNodes", "OnDestroyed");
	ScoreLabel.visible = false;
	VictoryLabel.visible = true;
	
	var FoundNodes = get_tree().get_nodes_in_group("DamageableNodes");
