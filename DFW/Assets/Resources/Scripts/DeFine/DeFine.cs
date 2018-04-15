using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

        Client_socket c_client = new Client_socket(ip_address, port);
    }
}
