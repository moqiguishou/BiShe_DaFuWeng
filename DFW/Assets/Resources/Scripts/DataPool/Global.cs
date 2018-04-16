using System.Collections;
using System.Collections.Generic;
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
    public string user_name { set; get; }
    public string password { set; get; }

    public void object_login_Send() {
        string toJson = "{\"user_name\":\"" + user_name + "\",\"password\":\"" + password+"\"}";
        Debug.Log(toJson);
        DeFine.rwSendMessage(toJson);
    }
}
