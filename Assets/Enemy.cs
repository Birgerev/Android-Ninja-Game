using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public bool falling;
    public float knockbackScale;
    public Vector2 dashScale;

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

    public void Knockback(Vector2 dir, Vector2 strength)
    {
        print(dir);
        rb.velocity = new Vector2(dir.x * strength.x, strength.y);
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("Enemy"))
        {
            if (triggerframes > 20)
            {
                Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                Knockback(dir, new Vector2(knockbackScale, knockbackScale));
                triggerframes = 0;
            }
        }

        if (col.name.Contains("Ninja"))     //For Player Collisions
        {
            if (triggerframes > 20)
            {
                //Dash Collision
                if (col.gameObject.GetComponent<Player>().dashing)
                {
                    Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                    Knockback(dir, dashScale);
                    triggerframes = 0;
                }
                else
                {
                    //TODO  knock player over
                }
            }
        }
    }
}
