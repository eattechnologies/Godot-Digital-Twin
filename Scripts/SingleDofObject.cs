using Godot;
using System;

public partial class SingleDofObject : Node3D
{
	// Godot units per PLC unit. The PLC sends mm and the robot models are 1 unit = 100 mm, so 0.01.
	[Export]
	private Vector3 TranslactionGain;
	[Export]
	private Vector3 RotationGain;

	// Added to the PLC value before applying the gain (PLC units, mm).
	// Use it to line up the PLC zero with the model's home position.
	[Export]
	private float ValueOffset;

	[Export]
	private CommData commData;

	[Export]
	private int variableIndex;

	// Pose set in the editor. The PLC value moves the node relative to it,
	// so each axis keeps its anchor on the parent carriage.
	private Vector3 homePosition;
	private Vector3 homeRotation;

	public override void _Ready()
	{
		homePosition = Position;
		homeRotation = Rotation;
	}

	public override void _Process(double delta)
	{
		if (variableIndex >= 0 && variableIndex < commData.ReceiveVariables.Length)
		{
			float value = commData.ReceiveVariables[variableIndex] + ValueOffset;
			Position = homePosition + TranslactionGain * value;
			Rotation = homeRotation + RotationGain * value * (float)Math.PI / 180.0f;
		}
	}
}
