using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Reflection;

public class UI_root : MonoBehaviour {

    GComponent com_main; //主界面
    GComponent com_start;//开始三选界面
    GComponent com_login;//登入界面
    GComponent com_room;//房间界面

    GComponent com_user;//user组件
    //主界面3个选项button
    GButton btn_to_begin;
    GButton btn_to_login;
    GButton btn_to_registe;

    GButton btn_back; //返回3选界面 按钮
    GButton btn_login;
    GButton btn_registe;
    // 控制器
    Controller main_jiemian;

    Controller open_login;
    Controller login_registe;
    //登入界面，输入文本
    GTextInput input_user_name;
    GTextInput input_password;

    GTextField text_room_username;
    GTextField text_room_userid;
    GTextField text_room_userlevel;

    //====================================================//
    Socket client_socket;
    Socket server_socket;

    static Socket s; 
    Client_socket my_client;

    string user_name = "";
    string password = "";
    //static DeFine rw = new DeFine();
    private static Hashtable huan_cun = new Hashtable();
    //private static int num_change = 0;
    private static bool isrun = true;
    Thread ti;

    

    // Use this for initialization
    void Start () {
        init_ui();
        init_button();
        init_controller();
        init_input();
        init_text();
        //一开始就创建client
        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        my_client = new Client_socket(client_socket);
        my_client.client_connet();
        s = my_client._socket();
        //
        huan_cun.Clear();
        //启动监听
        //my_listen();
        ti = new Thread(new ThreadStart(my_listen));
        ti.Start();
    }
    private void init_text() {
        text_room_username = com_user.GetChild("name").asTextField;
        text_room_userid = com_user.GetChild("id").asTextField;
        text_room_userlevel = com_user.GetChild("level").asTextField;
    }

    private void init_input()
    {
        input_user_name = com_login.GetChild("input_user_name").asTextInput;
        input_password = com_login.GetChild("input_password").asTextInput;
        
    }

    private void init_controller()
    {
        main_jiemian = com_main.GetController("JieMian");

        open_login = com_start.GetController("show_login");
        login_registe = com_login.GetController("login_registe");
    }

    private void init_ui()
    {
        UIPackage.AddPackage("UI/Gui_DFW");
        com_main = UIPackage.CreateObject("Gui_DFW", "Main").asCom;
        GRoot.inst.AddChild(com_main);

        float Pos_x_MainCom = (Screen.width - com_main.size.x) / 2;
        float Pos_y_MainCom = (Screen.height - com_main.size.y) / 2;
        com_main.SetXY(Pos_x_MainCom, Pos_y_MainCom);

        com_start = com_main.GetChild("Start").asCom;
        com_login = com_start.GetChild("login_registe").asCom;
        com_room = com_main.GetChild("Room").asCom;

        com_user = com_room.GetChild("user_self").asCom;
    }

    private void init_button()
    {
        btn_to_begin = com_start.GetChild("btn_begin").asButton;
        btn_to_login = com_start.GetChild("btn_login").asButton;
        btn_to_registe = com_start.GetChild("btn_registe").asButton;

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
        Object_login o_login = new Object_login(input_user_name.text, input_password.text, "registe");

        string json_str = JsonConvert.SerializeObject(o_login);
        Debug.Log("打包的数据：" + json_str);
        my_client.client_send(json_str);
    }
    public void registe_succeed()
    {
        Debug.Log("注册成功");
        login_registe.selectedIndex = 0;
    }
    public void registe_failure()
    {
        Debug.Log("注册失败");
    }

    private void Click_login()
    {
        Object_login o_login = new Object_login(input_user_name.text, input_password.text,"login");

        string json_str = JsonConvert.SerializeObject(o_login);
        Debug.Log("打包的数据："+json_str);
        my_client.client_send(json_str);
    }
    public void login_succeed() {
        Debug.Log("登入成功");
        huan_cun["isLogin"] = true;
        Debug.Log("修改缓存isLogin==>" + huan_cun["isLogin"]);
        huan_cun["ling_beginroom"] = true;
        Debug.Log("修改缓存ling_beginroom==>" + huan_cun["ling_beginroom"]);
    }
    public void login_failure() {
        Debug.Log("登入失败");
        huan_cun["isLogin"] = false;
    }


    //返回三选界面
    private void Click_back()
    {
        if (open_login.selectedIndex == 1) {
            open_login.SetSelectedIndex(0);
        }
    }

    //开始游戏
    private void Click_to_begin(){
        Debug.Log(ti.IsAlive);
        //if (huan_cun.ContainsKey("isLogin")) {
        //    Debug.Log("临时test" + huan_cun["isLogin"]);
        //    if ((bool)huan_cun["isLogin"]) {
        //        Debug.Log("开始游戏");
        //        huan_cun["ling_beginroom"] = true;
        //        return;
        //    }
        //}
        //Object_begin_room o_begin_room = new Object_begin_room();
        //string json_str = JsonConvert.SerializeObject(o_begin_room);

        //Debug.Log("打包的数据:" + json_str);
        //my_client.client_send(json_str);
        text_room_username.text = "moqiazu";
        //Debug.Log("开始游戏失败，需先登入游戏");
    }
    public void begin_room_succeed()
    {
        Debug.Log("进入房间选择界面成功");
        main_jiemian.selectedIndex = 1;
    }
    public void begin_room_failure()
    {
        Debug.Log("进入房间选择界面登入失败");
    }
    //private static void ready_to_room() {
    //    //num_change = 3;
    //    huan_cun["change1"] = "room_user_name";
    //    huan_cun["room_user_name"] =
    //    huan_cun[""];
    //}


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

    private static void my_listen() {

        while (isrun)
        {
            //接收消息
            string recStr = "";
            byte[] recBytes = new byte[1024];
            int bytes = s.Receive(recBytes);
            recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
            Debug.Log(recStr);

            Object_recive o_recive = JsonConvert.DeserializeObject<Object_recive>(recStr);
            func(o_recive);
        }
    }

    //private void my_listen() {
    //    string ip_address = "192.168.1.100";
    //    IPAddress ip = IPAddress.Parse(ip_address);
    //    int port = 8888;
    //    IPEndPoint ipe = new IPEndPoint(ip, port);
    //    server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    server_socket.Bind(ipe);
    //    Console.WriteLine("==绑定端口{0}" + ipe);
    //    //开始监听
    //    server_socket.Listen(1);//设置最大监听数为1
    //    Console.WriteLine("==Server Start and Waitting connection==");
    //    Socket c_socket = server_socket.Accept();

    //    //接收来自客户端的消息
    //    string name = "";
    //    byte[] recbyte = new byte[1024];
    //    int bytes = c_socket.Receive(recbyte, recbyte.Length, 0);//字节流 
    //    name += Encoding.ASCII.GetString(recbyte, 0, bytes);          //--> 字符流
    //    Console.WriteLine("====={0} 连接成功=====", name);
    //}

    //public Socket get_client() {
    //    return client_socket;
    //}

    //public void close_client() {
    //    client_socket.Close();
    //}

    // Update is called once per frame
    void Update () {
        if (huan_cun.ContainsKey("isLogin")&&(bool)huan_cun["isLogin"]) {
            if (huan_cun.ContainsKey("ling_beginroom") && (bool)huan_cun["ling_beginroom"]) {
                main_jiemian.selectedIndex = 1;
                huan_cun["ling_beginroom"] = false;
                Debug.Log("命令：开始进入房间模式，取消命令huan_cun[\"ling_beginroom\"]==>" + huan_cun["ling_beginroom"]);

            }
        }

        if (huan_cun.ContainsKey("num_change") && (int)huan_cun["num_change"] > 0) {
            text_room_username.text = (string)huan_cun["room_username"];
            text_room_userid.text = (string)huan_cun["room_userid"];
            //string level = huan_cun["room_userlevel"].ToString();
            text_room_userlevel.text = huan_cun["room_userlevel"].ToString();
            huan_cun["num_change"] = 0;
            Debug.Log(string.Format("修改user对象值:name=>{0};id=>{1};level=>{2}", huan_cun["room_username"], huan_cun["room_userid"], huan_cun["room_userlevel"]));

        }

    }
    
    //public void room_username() {
    //    //text_room_username.text = (string)huan_cun["room_username"];
    //    Debug.Log("调用方法room_username成功");
    //}
    //public void room_userid()
    //{
    //    text_room_userid.text = (string)huan_cun["room_userid"];
    //    Debug.Log("调用方法room_userid成功");
    //}
    //private void room_userlevel()
    //{
    //    text_room_userlevel.text = (string)huan_cun["room_userlevel"];
    //    Debug.Log("调用方法room_userlevel成功");
    //}


    private void test() {
        Debug.Log("@@@@@");
    }


    public static void func(Object_recive o_recive)
    {

        Type t = typeof(UI_root);
        MethodInfo mi;
        string func_name = "link_" + o_recive.type;
        mi = t.GetMethod(func_name);
        if (mi == null)
        {
            Debug.Log("未找到该回调函数");
            return;
        }
        else
        {
            UI_root ui = new UI_root();
            Debug.Log("开始执行回调函数:" + func_name);
            mi.Invoke(ui, new object[] {o_recive});
        }

    }

    public void link_login(Object_recive o_recive)
    {
        switch (o_recive.result)
        {
            case "succeed":
                login_succeed();

                huan_cun["num_change"] = 3;
                huan_cun["change1"] = "room_username";
                huan_cun["change2"] = "room_userid";
                huan_cun["change3"] = "room_userlevel";

                huan_cun["room_username"] = o_recive.user_name;
                huan_cun["room_userid"] = o_recive.user_id;
                huan_cun["room_userlevel"] = o_recive.level;

                break;
            case "failure":
                login_failure();
                break;
        }
    }

    public void link_registe(Object_recive o_recive)
    {
        Debug.Log("回调link_login函数");
        switch (o_recive.result)
        {
            case "succeed":
                registe_succeed();
                break;
            case "failure":
                registe_failure();
                break;
        }
    }

    public void link_begin_room(Object_recive o_recive)
    {
        Debug.Log("回调link_login函数");
        switch (o_recive.result)
        {
            case "succeed":
                begin_room_succeed();
                break;
            case "failure":
                begin_room_failure();
                break;
        }
    }
}
