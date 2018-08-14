using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject player;

    public bool lockX;
    public bool lockY;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            player = GameObject.Find("Ninja");

        if(player != null)
        {
            Vector2 pos = player.transform.position;
            if (!lockX)
                pos = new Vector2(0, pos.y);
            if (!lockY)
                pos = new Vector2(pos.x, 0);

            GetComponent<RigidbodyPixel>().position = pos;
        }
    }
}
