using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;

public class DeFine{
    public static void rwLoadScene(int sceneId) {
        SceneManager.LoadScene(sceneId);
        if (true) {

        }
    }

    public static void rwSendMessage(string json_str)
    {
        string ip_address = Global_value.IpAddress;
        int port = Global_value.Port;

        UI_root ui = new UI_root();
        Socket c_socket = ui.get_client();
        
    }
}
