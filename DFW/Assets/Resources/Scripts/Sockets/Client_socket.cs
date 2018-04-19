using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client_socket{
    string ip_address = "";
    int port;
    IPEndPoint ipe;
    Socket c_socket;

    public Client_socket(Socket c_socket) {
        this.c_socket = c_socket;

        IPAddress ip = IPAddress.Parse("192.168.147.128");
        ipe = new IPEndPoint(ip, 8888);
    }

    public Client_socket(string ip_address, int port) {
        this.ip_address = ip_address;
        this.port = port;
        IPAddress ip = IPAddress.Parse(ip_address);
        ipe = new IPEndPoint(ip, port);
        c_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            c_socket.Connect(ipe);
            //client_recive();
        }
        catch (System.Exception)
        {
            connect_fail();
            Debug.Log("连接失败..");
        }
    }

    public void client_connet()
    {
        try
        {
            c_socket.Connect(ipe);
        }
        catch (Exception)
        {
            connect_fail();
            Debug.Log("连接失败..");
        }
    }

    private void connect_fail() {

    }

    public void client_send(string json_str) {
        Debug.Log("发送消息："+json_str);
        byte[] send_byte = Encoding.ASCII.GetBytes(json_str);
        c_socket.Send(send_byte);

        //接收消息
        string recStr = "";
        byte[] recBytes = new byte[1024];
        int bytes = c_socket.Receive(recBytes);
        recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
        Debug.Log(recStr);
    }

    public void client_recive() {
        ////接收消息
        //string recStr = "";
        //byte[] recBytes = new byte[1024];
        //int bytes = c_socket.Receive(recBytes);
        //recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
        //Debug.Log(recStr);
    }
}
