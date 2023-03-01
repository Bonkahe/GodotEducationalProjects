using Godot;
using System;

public partial class DamageableEventNode : DamageableNode
{
	[Export] public Tween.TransitionType TransitionType { get; set; }

    [Export] public Vector3 ScaleSize { get; set; }

    [Export] public float RotationDegrees { get; set; }

    [Export] public int HitPoints { get; private set; } = 0;
    [Export] public Node3D ContainerNode { get; private set; }

    [Signal] public delegate void OnDestructionEventHandler();

    private bool EndOfGame;

    private Vector3 OriginalPosition;

    private Vector3 OriginalRotation;

    private Vector3 OriginalScale;

    private Tween newTween;

    public override void _Ready()
    {
		OriginalPosition = MeshReference.GlobalPosition;
        OriginalRotation = MeshReference.GlobalRotationDegrees;
        OriginalScale = MeshReference.Scale;
    }

    public void OnDestroyed()
    {
        EndOfGame = true;
        HitPoints = 0;
        OnHit();
    }

    public void OnCompleteDestruction()
    {
        ContainerNode.QueueFree();
    }

    public override void OnHit()
	{
        if (newTween != null && newTween.IsRunning())
        {
            newTween.Stop();
        }

        HitPoints -= 1;

        MeshReference.MaterialOverride = HitMaterial;

        newTween = CreateTween();

		newTween.SetTrans(TransitionType);

		newTween.TweenProperty(
			MeshReference,
			"global_position",
			MeshReference.GlobalPosition + new Vector3(0, 1, 0),
			FlashDuration / 2);

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        newTween.Parallel();
        newTween.TweenProperty(
            MeshReference,
            "global_rotation_degrees",
            OriginalRotation
                + new Vector3(
                    Mathf.Wrap(rng.RandfRange(-RotationDegrees, RotationDegrees), -180, 180),
                    Mathf.Wrap(rng.RandfRange(-RotationDegrees, RotationDegrees), -180, 180), 
                    Mathf.Wrap(rng.RandfRange(-RotationDegrees, RotationDegrees), -180, 180)),
            FlashDuration / 2);

        newTween.Parallel();
        newTween.TweenProperty(
            MeshReference,
            "scale",
            OriginalScale * ScaleSize,
            FlashDuration / 2);

        //Divider ------

        newTween.TweenProperty(
            MeshReference,
            "global_position",
            OriginalPosition,
            FlashDuration / 2);


        newTween.Parallel();
        newTween.TweenProperty(
            MeshReference,
            "global_rotation_degrees",
            OriginalRotation,
            FlashDuration / 2);

        newTween.Parallel();
        newTween.TweenProperty(
            MeshReference,
            "scale",
            OriginalScale,
            FlashDuration / 2);

        if (EndOfGame)
        {
            newTween.Connect("finished", Callable.From(() => OnCompleteDestruction()));
        }
        else
        {
            newTween.Connect("finished", new Callable(this, "OnReset"));
            if (HitPoints <= 0)
            {
                EmitSignal(SignalName.OnDestruction);
            }
            else
            {
                GetTree().CallGroup("ScoringGroup", "OnPointsAdded", 1);
            }
        }

        newTween.Play();
	}
}
