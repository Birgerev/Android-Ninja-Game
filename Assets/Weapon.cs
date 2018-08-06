using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Vector3 direction;
    public GameObject owner;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (owner != col.gameObject)
        {
            if (col.GetComponent<Enemy>() != null)
            {
                col.GetComponent<Enemy>().Knockback(direction, 
                    new Vector2(col.GetComponent<Enemy>().knockbackScale, col.GetComponent<Enemy>().knockbackScale));

                Destroy(gameObject);
            }
            else if (col.GetComponent<Player>() != null)
            {
                //TODO make player knock over
                Destroy(gameObject);
            }
        }
    }
}
