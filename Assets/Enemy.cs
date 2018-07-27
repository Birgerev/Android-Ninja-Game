using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public bool falling;
    public float knockbackScale;

    RigidbodyPixel rb;
    int triggerframes = 0;

    // Use this for initialization
    void Start () {
        rb = transform.GetComponent<RigidbodyPixel>();

	}
	
	// Update is called once per frame
	void Update () {
        triggerframes++;

    }

    public void Knockback(Vector2 dir)
    {
        print(dir);
        rb.velocity = new Vector2(dir.x * knockbackScale, knockbackScale);
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("Enemy"))
        {
            if (triggerframes > 20)
            {
                print("col");
                Vector2 dir = new Vector2(-1, 0);
                if (transform.position.x - col.transform.position.x > 0)
                    dir = new Vector2(1, 0);
                Knockback(dir);
                triggerframes = 0;
            }
        }
    }
}
