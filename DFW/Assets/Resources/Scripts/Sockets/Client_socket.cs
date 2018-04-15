using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class Client_socket{
    string ip_address = "";
    int port;
    IPEndPoint ipe;
    Socket c_socket;

    public Client_socket(string ip_address, int port) {
        this.ip_address = ip_address;
        this.port = port;
        IPAddress ip = IPAddress.Parse(ip_address);
        ipe = new IPEndPoint(ip, port);
        c_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            c_socket.Connect(ipe);
        }
        catch (System.Exception)
        {
            connect_fail();
            Debug.Log("连接失败..");
        }
    }

    private void connect_fail()
    {
    }
}
