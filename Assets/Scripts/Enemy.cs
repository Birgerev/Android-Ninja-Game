using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public bool falling;
    public float knockbackScale;
    public Vector2 dashScale;
    public Vector2 forward;
    public GameObject coin;
    public int enemyLevel = 0;
    public float jumpForce = 0.75f;

    Animator anim;
    RigidbodyPixel rb;
    int triggerframes = 0;
    int fallframes = 0;

    float health = 10;

    // Use this for initialization
    void Start () {
        rb = GetComponent<RigidbodyPixel>();
        anim = GetComponent<Animator>();
        StartCoroutine(Run());
        StartCoroutine(Actionloop());
	}
	
	// Update is called once per frame
	void Update () {
        triggerframes++;
        fallframes++;

        anim.SetInteger("level", enemyLevel);
        transform.GetComponent<SpriteRenderer>().flipX = (forward.x < 0) ? true : false;

        if (fallframes > 4)
            if (rb.grounded)
            {
                falling = false;

                if(enemyLevel == 1)
                    anim.SetBool("action", false);
            }

        //Run();
    }

    public void action()
    {
        if (enemyLevel == 0)
        {

        }
        else if (enemyLevel == 1)
        {
            Jump();
        }
        else if (enemyLevel == 2)
        {

        }
        else if (enemyLevel == 3)
        {

        }
    }

    IEnumerator Actionloop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if ((rb.grounded && !falling && rb.velocity.y == 0))
            {
                if(Random.Range(0, 10) > 8)
                {
                    action();
                }
            }
        }
    }

    IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if ((rb.grounded && !falling && rb.velocity.y == 0) || !rb.doGravity)
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
    
    public void Kill(bool anim)
    {
        Destroy(gameObject);
    }

    public void Damage(float amount)
    {
        health -= amount;

        print(health);

        if (health <= 0)
        {
            Kill(true);

            dropCoin(transform.position);
        }
    }

    public void Jump()
    {
        if (rb.grounded)
        {
            //Animation
            anim.SetBool("action", true);

            print("jump");

            rb.velocity += new Vector2(0, jumpForce);
        }
    }

    public void dropCoin(Vector2 pos)
    {
        GameObject obj = Instantiate(coin);
        obj.transform.position = pos;
        obj.name = "coin";
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
