using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public bool falling;
    public float knockbackScale;
    public Vector2 dashScale;
    public Vector2 forward;

    RigidbodyPixel rb;
    int triggerframes = 0;
    int fallframes = 0;

    // Use this for initialization
    void Start () {
        rb = transform.GetComponent<RigidbodyPixel>();
        StartCoroutine(Run());
	}
	
	// Update is called once per frame
	void Update () {
        triggerframes++;
        fallframes++;

        transform.GetComponent<SpriteRenderer>().flipX = (forward.x < 0) ? true : false;

        if (fallframes > 4)
            if (rb.grounded)
                falling = false;
        
        Run();
    }

    IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (rb.grounded && !falling && rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(speed * forward.x, rb.velocity.y);
            }
        }
    }

    public void Knockback(Vector2 dir, Vector2 strength)
    {
        rb.velocity = new Vector2(dir.x * strength.x, strength.y);
        falling = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("Enemy"))
        {
            if (triggerframes > 2)
            {
                Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                Knockback(dir, new Vector2(knockbackScale, knockbackScale));
                triggerframes = 0;
            }
        }

        if (col.name.Contains("Kick"))     //For Player Collisions
        {
            if (triggerframes > 2)
            {
                //Dash Collision
                Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                Knockback(dir, dashScale);
                triggerframes = 0;
            }
        }

        if (col.name.Contains("Ninja"))     //For Player Collisions
        {
            if (triggerframes > 2)
            {
                //Dash Collision
                Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                Knockback(dir, dashScale);
                triggerframes = 0;
            }
        }
    }
}
