using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Manager manager;     //Manager Script
    public GameObject weapon; //Weapon Prefab, usually asigned by the Manager script
    RigidbodyPixel physics; //Rigidbody2D

    public float jumpForce = 0.75f;
    public float jumpTimeInAir = 1f;

    public float dashForce = 0.25f;
    public bool dashing;

    public float knockbackScale;
    public bool falling = false;

    public float dashFriction;

    public bool knockback;  //For Debuging knockback power
    //private variables
    bool scheduledThrow = false; //waits for player to land on ground or for gravity to turn off before using weapon
    Vector2 scheduledThrowDirection = Vector2.zero;
    int fallingframes = 0;
    int dashframes = 0;

    void Start ()
    {
        physics = transform.GetComponent<RigidbodyPixel>();
    }
	
	void Update () {
        if (scheduledThrow)     //Try Throwing weapon if scheduled, will continiue til' success
            Throw(scheduledThrowDirection);

        fallingframes++;
        dashframes++;

        if(falling && physics.grounded && fallingframes > 5)
        {
            falling = false;
            fallingframes = 0;
        }

        //Friction
        if (physics.grounded && !falling)
        {
            if (dashing)
            {
                physics.velocity *= dashFriction;
            }
            else
            {
                physics.velocity = new Vector2(0, physics.velocity.y);
            }
        }

        if (dashing)
        {
            if (physics.velocity.x < 0.05f && physics.velocity.x > -0.05f && dashframes > 2)
            {
                dashing = false;
            }
            else
            {

            }
        }

        if (knockback)  //Debug of knockback power
        {
            knockback = false;
            Knockback(new Vector2(1,0));
        }
        
        Controls();
	}

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Dash(new Vector2(-1, 0));
        if (Input.GetKeyDown(KeyCode.D))
            Dash(new Vector2(1, 0));

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public void Dash(Vector2 dir)
    {
        if (physics.grounded)
        {
            dashframes = 0;
            dashing = true;
            physics.velocity += new Vector2(dashForce * dir.x, 0);
        }
    }

    public void Jump()
    {
        if (physics.grounded)
        {
            physics.velocity += new Vector2(0, jumpForce);
            StartCoroutine(jump());
        }
    }

    IEnumerator jump()
    {
        while (physics.velocity.y > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        physics.velocity = Vector3.zero;
        physics.doGravity = false;
        
        yield return new WaitForSeconds(jumpTimeInAir);

        physics.doGravity = true;
    }

    public void Throw(Vector2 dir)
    {
        if (physics.grounded || !physics.doGravity)//Allow Throw If On Ground or Frozen in air
        {
            Vector3 spawner = transform.Find((dir.x == 1) ? "right" : "left").transform.position;

            GameObject obj = Instantiate(weapon);
            obj.transform.position = spawner;

            RigidbodyPixel rb = obj.GetComponent<RigidbodyPixel>();
            rb.position = spawner;

            Weapon wep = obj.GetComponent<Weapon>();
            wep.direction = dir;
            wep.owner = gameObject;

            scheduledThrow = false;
        }
        else
        {
            scheduledThrow = true; //Schedule Throw for later
            scheduledThrowDirection = dir;
        }
    }
    
    public void Knockback(Vector2 dir)
    {
        falling = true;
        fallingframes = 0;
        physics.velocity = new Vector2(dir.x * knockbackScale, 0.5f);
    }
}