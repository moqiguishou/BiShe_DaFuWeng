using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject hhh = (GameObject)Resources.Load("Prefabs/蓝格子");
        Debug.Log(hhh);
        GameObject h1 = Instantiate(hhh, new Vector3(1, 2, 3), transform.rotation);
        GameObject h2 = Instantiate(hhh, new Vector3(2, 2, 3), transform.rotation);
        GameObject h3 = Instantiate(hhh, new Vector3(5, 2, 3), transform.rotation);
        GameObject[] h = new GameObject[10];
        for (int i = 0; i < h.Length; i++) {
            h[i] = Instantiate(hhh);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
