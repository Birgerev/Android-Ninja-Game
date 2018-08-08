using System.Collections;
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
        if (!col.name.Contains("Kick"))
        {
            GameObject obj = Instantiate(splash);
            obj.transform.position = new Vector2(col.transform.position.x, yLevel);

            print(col.name);
            if (col.gameObject.name.Contains("Ninja"))
            {
                Destroy(col.gameObject);
                Manager.instance.Lose();
            }
            else
            {
                //Destroy anything but the player
                Destroy(col.gameObject);
                Manager.instance.score++;
            }
        }
    }
}
