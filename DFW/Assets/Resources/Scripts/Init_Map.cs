using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Init_Map : MonoBehaviour {

    string testJson = "{\"MapName\":\"testmap\",\"Detail\":["+
        "{\"Pos_x\":8,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":9,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":10,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":11,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":12,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":13,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":14,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":15,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":16,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":17,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":18,\"Pos_y\":0,\"Pos_z\":11,\"prefab_path\":\"Prefabs/蓝格子\"}," +

        "{\"Pos_x\":18,\"Pos_y\":0,\"Pos_z\":12,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":18,\"Pos_y\":0,\"Pos_z\":13,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":18,\"Pos_y\":0,\"Pos_z\":14,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":18,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +

        "{\"Pos_x\":17,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":16,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":15,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":14,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":13,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":12,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":11,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":10,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":9,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":8,\"Pos_y\":0,\"Pos_z\":15,\"prefab_path\":\"Prefabs/蓝格子\"}," +

        "{\"Pos_x\":8,\"Pos_y\":0,\"Pos_z\":14,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":8,\"Pos_y\":0,\"Pos_z\":13,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "{\"Pos_x\":8,\"Pos_y\":0,\"Pos_z\":12,\"prefab_path\":\"Prefabs/蓝格子\"}," +
        "]}";
    Table_Map my_table_map;

    void Start () {
        my_table_map = JsonConvert.DeserializeObject<Table_Map>(testJson);
        draw_map();
    }

    private void draw_map() {
        int length_detail = my_table_map.Detail.Count;
        for (int i = 0; i < length_detail; i++)
        {
            string prefab_path = my_table_map.Detail[i].prefab_path;
            GameObject prefab_GeZi = (GameObject)Resources.Load(prefab_path);
            Debug.Log(prefab_GeZi);
            int x = (my_table_map.Detail[i].Pos_x - 1)* Global_value.BiLi_GeZi_XY;
            int y = my_table_map.Detail[i].Pos_y * Global_value.BiLi_GeZi_XY;
            int z = (my_table_map.Detail[i].Pos_z - 1) * Global_value.BiLi_GeZi_XY;
            GameObject GeZi = Instantiate(prefab_GeZi, new Vector3(x,y,z), transform.rotation);
        }

        
    }
   
    // Update is called once per frame
    void Update () {
		
	}
}

public class Table_Map {
    public string MapName { set; get; }
    public List<List_Detail> Detail { set; get; }
}

public class List_Detail {
    public int Pos_x { set; get; }
    public int Pos_y { set; get; }
    public int Pos_z { set; get; }
    public string prefab_path { set; get; }
}
