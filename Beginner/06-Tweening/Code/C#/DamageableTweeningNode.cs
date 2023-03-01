using Godot;
using System;

public partial class DamageableTweeningNode : DamageableNode
{
	[Export] public Tween.TransitionType TransitionType { get; set; }

    [Export] public Vector3 ScaleSize { get; set; }

    [Export] public float RotationDegrees { get; set; }

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

    public override void OnHit()
	{
        if (newTween != null && newTween.IsRunning())
        {
            newTween.Stop();
        }

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


        newTween.Connect("finished", new Callable(this, "OnReset"));

        newTween.Play();
	}
}
