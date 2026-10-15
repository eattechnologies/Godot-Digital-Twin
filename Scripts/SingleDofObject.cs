using Godot;
using System;

public partial class SingleDofObject : Node3D
{
	[Export]
	private Vector3 TranslactionGain;
	[Export]
	private Vector3 RotationGain;


	[Export]
	private CommData commData;

	[Export]
	private int variableIndex;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (variableIndex >= 0 && variableIndex < commData.ReceiveVariables.Length)
		{
			this.Position = TranslactionGain * commData.ReceiveVariables[variableIndex];
			this.Rotation = RotationGain * commData.ReceiveVariables[variableIndex] * (float) Math.PI/180.0f;
		
		}
	}
}
