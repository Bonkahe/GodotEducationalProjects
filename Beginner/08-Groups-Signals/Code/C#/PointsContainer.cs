using Godot;
using Godot.Collections;
using System;

public partial class PointsContainer : Node
{
	[Export] public Label ScoreLabel { get; private set; }
    [Export] public Label VictoryLabel { get; private set; }

    private int CurrentPoints = 0;
    private bool HasWon = false;

    public void OnPointsAdded(int points)
    {
        CurrentPoints += points;
        ScoreLabel.Text = "Score:" + CurrentPoints.ToString();
    }

    public void OnVictory()
    {
        if (HasWon)
        {
            return;
        }
        

        HasWon = true;
        GetTree().CallGroup("DamageableNodes", "OnDestroyed");
        ScoreLabel.Visible = false;
        VictoryLabel.Visible = true;

        Array<Node> FoundNodes = GetTree().GetNodesInGroup("DamageableNodes");
    }
}
