using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;
using System.Net;
using System.Net.Sockets;

public class UI_root : MonoBehaviour {
    GComponent com_main; //主界面
    GComponent com_login;//登入界面
    //主界面3个选项button
    GButton btn_to_begin;
    GButton btn_to_login;
    GButton btn_to_registe;

    GButton btn_back; //返回3选界面 按钮
    GButton btn_login;
    GButton btn_registe;
    // 控制器
    Controller open_login;
    Controller login_registe;
    //登入界面，输入文本
    GTextInput input_user_name;
    GTextInput input_password;

    //====================================================//
    Socket client_socket;
    Client_socket my_client;

    string user_name = "";
    string password = "";

    // Use this for initialization
    void Start () {
        init_ui();
        init_button();
        init_controller();
        init_input();
        //一开始就创建client
        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        my_client = new Client_socket(client_socket);
        my_client.client_connet();
    }

    private void init_input()
    {
        input_user_name = com_login.GetChild("input_user_name").asTextInput;
        input_password = com_login.GetChild("input_password").asTextInput;
        
    }

    private void init_controller()
    {
        open_login = com_main.GetController("show_login");
        login_registe = com_login.GetController("login_registe");
    }

    private void init_ui()
    {
        UIPackage.AddPackage("UI/Gui_DFW");
        com_main = UIPackage.CreateObject("Gui_DFW", "Main").asCom;
        GRoot.inst.AddChild(com_main);

        float Pos_x_MainCom = (Screen.width - com_main.size.x) / 2;
        float Pos_y_MainCom = (Screen.width - com_main.size.y) / 2;
        com_main.SetXY(Pos_x_MainCom, Pos_y_MainCom);
        com_login = com_main.GetChild("login_registe").asCom;
    }

    private void init_button()
    {
        btn_to_begin = com_main.GetChild("btn_begin").asButton;
        btn_to_login = com_main.GetChild("btn_login").asButton;
        btn_to_registe = com_main.GetChild("btn_registe").asButton;

        btn_back = com_login.GetChild("btn_back").asButton;
        btn_login = com_login.GetChild("btn_login").asButton;
        btn_registe = com_login.GetChild("btn_registe").asButton;
        //添加按钮事件
        btn_to_begin.onClick.Add(()=> { Click_to_begin(); });
        btn_to_login.onClick.Add(()=> { Click_to_login(); });
        btn_to_registe.onClick.Add(()=> { Click_to_registe(); });

        btn_back.onClick.Add(()=> { Click_back(); });
        btn_login.onClick.Add(() => { Click_login(); });
        btn_registe.onClick.Add(() => { Click_registe(); });
    }

    private void Click_registe()
    {
        throw new NotImplementedException();
    }

    private void Click_login()
    {
        user_name = input_user_name.text;
        password = input_password.text;
        Debug.Log("name:" + user_name + "password:" + password);

        string toJson = "{\"user_name\":\"" + user_name + "\",\"password\":\"" + password + "\"}";
        my_client.client_send(toJson);
    }

    //返回三选界面
    private void Click_back()
    {
        if (open_login.selectedIndex == 1) {
            open_login.SetSelectedIndex(0);
        }
    }

    //开始游戏
    private void Click_to_begin()
    {
        Debug.Log("开始游戏");
        DeFine.rwLoadScene(1);
        GRoot.inst.RemoveChild(com_main);
    }

    //登入
    private void Click_to_login()
    {
        Debug.Log("登入");
        open_login.SetSelectedIndex(1);
        if (login_registe.selectedIndex != 0) {
            login_registe.SetSelectedIndex(0);
        }
    }

    //注册
    private void Click_to_registe()
    {
        Debug.Log("注册");
        open_login.SetSelectedIndex(1);
        if (login_registe.selectedIndex != 1)
        {
            login_registe.SetSelectedIndex(1);
        }
    }

    public Socket get_client() {
        return client_socket;
    }

    public void close_client() {
        client_socket.Close();
    }
    // Update is called once per frame
    void Update () {
        
    }

    private void test() {
        Debug.Log("@@@@@");
    }
}
