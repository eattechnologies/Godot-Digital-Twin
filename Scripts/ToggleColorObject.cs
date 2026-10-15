using Godot;
using System;

public partial class ToggleColorObject : Node
{
	[Export]
	private CommData commData;
	[Export]
	private int variableIndex;

	[Export]
	private MeshInstance3D meshInstance3D;
	private Material material;

	[Export]
	private Color InactiveColor;
	[Export]
	private Color ActiveColor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (meshInstance3D == null) return;
		if (meshInstance3D.Mesh == null) return;

		material = meshInstance3D.Mesh.SurfaceGetMaterial(0);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (material == null) return;

		if (variableIndex >= 0 && variableIndex < commData.ReceiveVariables.Length)
		{
			
			material.Set("albedo_color", commData.ReceiveVariables[variableIndex] != 1.0f ? ActiveColor : InactiveColor);
		}
		
	}
}
