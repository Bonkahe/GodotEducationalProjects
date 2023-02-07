using Godot;
using System;

public partial class Damageable : Node
{
	[Export] public GeometryInstance3D MeshReference;
	[Export] public Material BaseMaterial;
    [Export] public Material HitMaterial;

	[Export] public float FlashDuration = 0.2f;



    public void OnHit()
	{
		MeshReference.MaterialOverride = HitMaterial;
		SceneTreeTimer timer = GetTree().CreateTimer(FlashDuration);
		timer.Connect("timeout", new Callable(this, "OnReset"));
	}

	public void OnReset()
	{
		MeshReference.MaterialOverride = BaseMaterial;
	}
}
