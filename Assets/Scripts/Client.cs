using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    private bool sockedReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public bool ConnectToServer(string host, int port)
    {
        if (sockedReady)
            return false;

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            sockedReady = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error " + e.Message);
        }

        return sockedReady;
    }

    private void Update()
    {
        if (sockedReady)
        {
            if(stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data !=null)
                    OnIncomingData(data);
            }
        }
    }

    public void Send(string data)
    {
        if (!sockedReady)
            return;

        writer.WriteLine(data);
        writer.Flush();
    }

    private void OnIncomingData(string data)
    {
        Debug.Log(data);
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }

    private void CloseSocket()
    {
        if (!sockedReady)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        sockedReady = false;
    }
}

public class GameClient
{
    public string name;
    public bool isHost;
}