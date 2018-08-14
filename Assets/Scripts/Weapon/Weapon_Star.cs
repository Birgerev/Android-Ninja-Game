using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Star : MonoBehaviour {

    public float speed;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.GetComponent<RigidbodyPixel>().velocity = 
            transform.GetComponent<Weapon>().direction * speed;
	}
}
