using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Manager manager;
    public RigidbodyPixel physics;
    public GameObject weapon;

    public float jumpForce = 0.75f;
    public float jumpTimeInAir = 1f;

    public float dashForce = 0.25f;

    bool scheduledThrow = false; //waits for player to land on ground or for gravity to turn off before using weapon
    Vector2 scheduledThrowDirection = Vector2.zero;


    void Start () {
        physics = transform.GetComponent<RigidbodyPixel>();

    }
	
	void Update () {
        if (scheduledThrow)
            Throw(scheduledThrowDirection);

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
        if(physics.grounded)
            physics.velocity += new Vector2(dashForce*dir.x, 0);
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

        physics.doGravity = false;
        
        yield return new WaitForSeconds(jumpTimeInAir);

        physics.doGravity = true;
    }

    public void Throw(Vector2 dir)
    {
        if (physics.grounded || !physics.doGravity)
        {
            Vector3 spawner = transform.Find((dir.x == 1) ? "right" : "left").transform.position;

            GameObject obj = Instantiate(weapon);
            obj.transform.position = spawner;

            RigidbodyPixel rb = obj.GetComponent<RigidbodyPixel>();
            rb.position = spawner;

            Weapon wep = obj.GetComponent<Weapon>();
            wep.direction = dir;

            scheduledThrow = false;
        }
        else
        {
            scheduledThrow = true; //Schedule Throw for later
            scheduledThrowDirection = dir;
        }
    }

    public void Die()
    {

    }
}