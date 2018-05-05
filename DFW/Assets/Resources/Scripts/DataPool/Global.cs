using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Global_GeZi
{
    public static int TypeGeZi_KongDi = 0;
    public static int TypeGeZi_FangZi = 1;
}

public class Global_value {
    public static int BiLi_GeZi_XY = 4;   //格子坐标比例

    //场景的ID
    public static int Scene_GameStart = 0; //start界面，开始、登入、注册
    public static int Scene_GameMain = 1; //main 游戏界面。

    //服务端IP port
    public static int Port = 8888; //端口
    public static string IpAddress = "192.168.177.128";//IP地址
}

//发送登录信息 对象
public class Object_login {
    public string user_name;
    public string password;
    public string type;

    public Object_login(string name,string password,string type) {
        this.user_name = name;
        this.password = password;
        this.type = type;
    }
}

//发送房间选择 对象
public class Object_begin_room {
    public string type = "begin_room";

}




//public delegate void link_func();
public class Object_recive {
    public string type;
    public string result;

    public string user_name;
    public string user_id;
    public int level;
}
