using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPixel : MonoBehaviour {

    public float pixelSize = 0.0625f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.x = pixelSize * Mathf.RoundToInt(pos.x / pixelSize);
        pos.y = pixelSize * Mathf.RoundToInt(pos.y / pixelSize);
        pos.z = 0;


        transform.position = pos;
	}
}
