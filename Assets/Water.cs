﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject splash;
    public float yLevel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = Instantiate(splash);
        obj.transform.position = new Vector2(col.transform.position.x, yLevel);
        
        if (col.gameObject.name.Contains("Ninja"))
        {

        }
        else
        {
            //Destroy anything but the player
            Destroy(col.gameObject);
        }
    }
}