using Godot;
using System;

public partial class DamageableNode : Node
{
	[Export] public MeshInstance3D MeshReference;
	[Export] public Material BaseMaterial;
    [Export] public Material HitMaterial;

	[Export] public float FlashDuration = 0.2f;



    public virtual void OnHit()
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
