using Godot;
using System;
using System.Net.Sockets;
using System.Threading;

public partial class TCPCommunication : Node
{
	// TCP
	private TcpClient socketConnection;
	private NetworkStream stream;
	private Thread clientReceiveThread;

	[Export]
	public String serverIpAddress = "127.0.0.1";
	[Export]
	public int serverPort = 50003;
	private bool isConnected;

	[Export]
	private CommData commData;

	private void ConnectToTcpServer()
	{
		try
		{

			socketConnection = new TcpClient(serverIpAddress, serverPort);
			socketConnection.ReceiveBufferSize = 1024;

			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();

			isConnected = true;
		}
		catch(Exception e)
		{
			isConnected = false;
			GD.Print(e);
		}
	}

	private void CloseConnection()
	{
		stream.Close();
		socketConnection.GetStream().Close();
		socketConnection.Close();
		isConnected = false;
	}

	private void ListenForData()
	{
		try
		{
			while (true)
			{
				using (stream = socketConnection.GetStream())
				{
					int length;
					while ((length = stream.Read(commData.ReceiveBytes, 0, commData.ReceiveBytes.Length)) != 0)
					{
						stream.Flush();
					}
				}
			}

		} catch (Exception e)
		{

			isConnected = false;
			GD.Print(e);
		}
	}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectToTcpServer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
