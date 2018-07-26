using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPixel : MonoBehaviour {

    public float pixelSize = 0.0625f;

    public bool doGravity = false;
    public float gravityScale = 0.08f;

    public bool grounded = false;
    public float groundLevel = -4f;

    public Vector2 velocity = Vector2.zero;
    public float velocitySpeed = 0.04f;
    public Vector3 position;
    public float velocityDropoff = 0.97f;
    public float xDropoff = 0.85f;
    
	void Start () {
        position = transform.position;
        StartCoroutine(loop());
	}
	
	void Update ()
    {
        //Pixel Snapping
        Vector3 pos = position;
        pos.x = pixelSize * Mathf.RoundToInt(pos.x / pixelSize);
        pos.y = pixelSize * Mathf.RoundToInt(pos.y / pixelSize);
        pos.z = 0;

        if (doGravity)
        {
            grounded = (transform.position.y <= groundLevel);

            if (grounded)
            {
                if(transform.position.y < groundLevel)
                pos.y = pixelSize * Mathf.RoundToInt(groundLevel / pixelSize);
                
                position = new Vector3(position.x, groundLevel);
            }
            //Make Ninja stand on ground



            if (grounded && velocity.y < 0)
            {
                velocity = new Vector2(velocity.x, 0);      //Reset Y velocity
            }
        }
        transform.position = pos;
    }

    IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / 30);
            if (doGravity)
            {
                Velocity();

                Gravity();

                VelocityDropoff();

            }
        }
    }

    void Velocity()
    {
        Vector3 vel = new Vector3();

        vel.x = velocity.x;
        vel.y = velocity.y;

        position += vel;
    }

    void Gravity()
    {
        if (!grounded)
            velocity += new Vector2(0, -gravityScale);
    }

    void VelocityDropoff()
    {
        velocity *= velocityDropoff;
        velocity = new Vector2(velocity.x * xDropoff, velocity.y);

        /*
        if (velocity.x != 0)
            velocity += new Vector2((velocity.x > 0) ? -velocityDropoff : velocityDropoff, 0);

        if (velocity.y > 0)
            velocity += new Vector2(0, -velocityDropoff);*/
    }
}
