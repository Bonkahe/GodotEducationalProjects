using Godot;
using System;

public partial class CameraCollider : Node3D
{
	[Export] public Node3D cameraNode;
	[Export] public RayCast3D rayCast3D;
	[Export] public float standOffDistance = 0.1f;

	[Export(PropertyHint.Layers2dPhysics)] public uint CameraColliderLayers;

	private Vector3 cameraLocalStartingPosition;

    public override void _Ready()
	{
		cameraLocalStartingPosition = ToLocal(cameraNode.GlobalPosition);
    }

	public override void _Process(double delta)
	{
		if (rayCast3D.IsColliding())
		{
			cameraNode.Position = new Vector3(cameraNode.Position.x, cameraNode.Position.y, rayCast3D.GetCollisionPoint().DistanceTo(rayCast3D.GlobalPosition) - standOffDistance);
		}
		else
		{
			cameraNode.GlobalPosition = rayCast3D.ToGlobal(rayCast3D.TargetPosition);
		}

		//RayCast();
	}

	private void RayCast()
	{
		PhysicsRayQueryParameters3D query = new()
		{
			From = GlobalPosition,
			To = ToGlobal(cameraLocalStartingPosition),
			CollideWithAreas = false,
			CollideWithBodies = true,
			CollisionMask = CameraColliderLayers
        };

		var hitDictionary = GetWorld3d().DirectSpaceState.IntersectRay(query);

		if (hitDictionary.Count > 0)
		{
            cameraNode.Position = new Vector3(cameraNode.Position.x, cameraNode.Position.y, ((Vector3)hitDictionary["position"]).DistanceTo(GlobalPosition) - standOffDistance);
        }
		else
		{
			cameraNode.GlobalPosition = ToGlobal(cameraLocalStartingPosition);
        }
	}
}
