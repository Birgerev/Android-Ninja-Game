using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour {

    public float time = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(destroy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
