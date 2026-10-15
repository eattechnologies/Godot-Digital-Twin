using Godot;
using System;
using System.Text;
using System.Collections.Generic;

public partial class CommData : Node
{
	[Export]
	private String dataHeader = "CRD";
	[Export]
public byte[] ReceiveBytes = new byte[1024];

[Export]
public float[] ReceiveVariables = new float[64]; // size to whatever max variable count you expect
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (CheckHeader(ReceiveBytes, dataHeader))
		{
			int size = (int) ReceiveBytes[dataHeader.Length];

			Buffer.BlockCopy(ReceiveBytes, dataHeader.Length + 1, ReceiveVariables, 0, size*4);

		}

		ResetReceiveBytes();

	}

	private bool CheckHeader(byte[] bytes, String header)
	{
		byte[] headerBytes = Encoding.ASCII.GetBytes(header);

		for (int i = 0; i < header.Length; i++)
		{
			if (headerBytes[i] != bytes[i]) return false;

		}

			return true;
	}

	private void ResetReceiveBytes()
	{
		for (int i = 0; i < ReceiveBytes.Length; i++)
		{
			ReceiveBytes[i] = 0;
		}
	}
}
