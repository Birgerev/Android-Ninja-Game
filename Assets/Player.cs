using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Manager manager;     //Manager Script
    public GameObject weapon; //Weapon Prefab, usually asigned by the Manager script
    Animator anim;
    RigidbodyPixel physics; //Rigidbody2D

    public float jumpForce = 0.75f;
    public float jumpTimeInAir = 1f;

    public float dashForce = 0.25f;
    public bool dashing;

    public Vector2 knockbackScale;
    public bool falling = false;

    public float dashFriction;

    private bool facing; //true if direction is left

    public int weaponAmount = 0;
    public float SwipeResistance = 0.7f;
    private bool weaponcooldown = false;

    //private variables
    bool scheduledThrow = false; //waits for player to land on ground or for gravity to turn off before using weapon
    Vector2 scheduledThrowDirection = Vector2.zero;
    int fallingframes = 0;
    int dashframes = 0;
    int triggerframes = 0;
    public int swipemodeframes;

    bool dashmode;

    GameObject leftkick;
    GameObject rightkick;

    void Start ()
    {
        physics = transform.GetComponent<RigidbodyPixel>();
        anim = transform.GetComponent<Animator>();
        manager = Manager.instance;

        leftkick = transform.Find("leftKick").gameObject;
        rightkick = transform.Find("rightKick").gameObject;
        gameObject.name = "Ninja";

        leftkick.SetActive(false);
        rightkick.SetActive(false);

        //Start Recharge Loop
        StartCoroutine(WeaponRecharge());
        StartCoroutine(Cooldown());
    }
	
	void Update () {
        if (scheduledThrow)     //Try Throwing weapon if scheduled, will continiue til' success
            Throw(scheduledThrowDirection);

        //Update frame timers
        fallingframes++;
        dashframes++;
        triggerframes++;

        //Animation
        anim.SetBool("Dashing", dashing);
        anim.SetBool("Grounded", physics.grounded);
        if (anim.GetBool("Falling") && physics.grounded)
            anim.SetBool("Falling", false);


        //Set Flip X in spriteRenderer
        transform.GetComponent<SpriteRenderer>().flipX = facing;

        //Fixes knockback friction bug
        if(falling && physics.grounded && fallingframes > 5)
        {
            falling = false;
            fallingframes = 0;
        }
        
        if (physics.grounded && !falling)
        {
            if (dashing)
            {
                //Dash Friction
                physics.velocity *= dashFriction;
            }
            else
            {
                //Disable Kick Triggers if not dashing
                leftkick.SetActive(false);
                rightkick.SetActive(false);

                //Remove x velocity
                physics.velocity = new Vector2(0, physics.velocity.y);
            }
        }

        if (dashing)
        {
            //Stop dashing if standing still
            if (physics.velocity.x < 0.05f && physics.velocity.x > -0.05f && dashframes > 2)
            {
                dashing = false;
            }
            else
            {

            }
        }
        
        Controls();
	}

    void Controls()
    {
        //Called For PC Controlls
        /*
        if (Input.GetKeyDown(KeyCode.A))
            Dash(new Vector2(-1, 0));
        if (Input.GetKeyDown(KeyCode.D))
            Dash(new Vector2(1, 0));


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            Jump();*/

        //Mobile Controlls
        //Dash

        int framesforswipe = 5;

        if (Input.GetMouseButton(0))
        {
            if(swipemodeframes < framesforswipe)
                swipemodeframes++;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (swipemodeframes >= framesforswipe)
            {
                Vector2 deltatouch = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dir = Vector2.zero;

                if (deltatouch.x < -(SwipeResistance*dashForce))
                    dir = new Vector2(1, 0);
                if (deltatouch.x > (SwipeResistance * dashForce))
                    dir = new Vector2(-1, 0);
                if (deltatouch.y < -(jumpForce*4))
                    dir = new Vector2(0, 1);
                if (deltatouch.y > (dashForce))
                    dir = new Vector2(0, -1);

                if (dir.x != 0)
                    Dash(dir);
                if (dir.y == 1)
                    Jump();
            }
            swipemodeframes = 0;
        }
    }

    public void Dash(Vector2 dir)
    {
        if (physics.grounded)
        {
            //Change Facing Direction
            facing = (dir.x < 0);

            ((dir.x > 0) ? rightkick : leftkick).SetActive(true);

            //Dash Behaviour
            dashframes = 0;
            dashing = true;
            physics.velocity += new Vector2(dashForce * dir.x, 0);
        }
    }

    public void Jump()
    {
        if (physics.grounded)
        {
            //Animation
            anim.SetTrigger("Jump");

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

        //Animation
        anim.SetBool("Falling", true);
        anim.SetBool("Disoriented", false);
    }

    IEnumerator WeaponRecharge()
    {
        weaponAmount = weapon.GetComponent<Weapon>().maxAmount;
        while (true)
        {
            yield return new WaitForSeconds(weapon.GetComponent<Weapon>().recharge);
            if(weaponAmount < weapon.GetComponent<Weapon>().maxAmount)
            {
                weaponAmount++;
            }
        }
    }

    IEnumerator Cooldown()
    {
        while (true)
        {
            if (weaponcooldown)
            {
                yield return new WaitForSeconds(weapon.GetComponent<Weapon>().cooldown);
                weaponcooldown = false;
            }
            else yield return new WaitForSeconds(0.05f);
        }
    }

    public void Throw(Vector2 dir)
    {
        if (Manager.instance.controllPlayer)
        {
            //Change Facing Direction
            facing = (dir.x < 0);
            if (weaponAmount > 0 && !weaponcooldown)
            {
                if (physics.grounded || !physics.doGravity)//Allow Throw If On Ground or Frozen in air
                {
                    //Animation
                    anim.SetTrigger("Throw");

                    Vector3 spawner = transform.Find((dir.x == 1) ? "right" : "left").transform.position;

                    GameObject obj = Instantiate(weapon);
                    obj.transform.position = spawner;

                    RigidbodyPixel rb = obj.GetComponent<RigidbodyPixel>();
                    rb.position = spawner;

                    Weapon wep = obj.GetComponent<Weapon>();
                    wep.direction = dir;
                    wep.owner = gameObject;

                    scheduledThrow = false;

                    weaponcooldown = true;
                    weaponAmount--;
                }
                else
                {
                    scheduledThrow = true; //Schedule Throw for later
                    scheduledThrowDirection = dir;
                }
            }
        }
    }
    
    public void Knockback(Vector2 dir, Vector2 strength)
    {
        falling = true;
        fallingframes = 0;
        physics.velocity = new Vector2(dir.x * strength.x, strength.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("Enemy"))     //For Player Collisions
        {
            if (triggerframes > 2 && !dashing)
            {
                //Dash Collision
                Vector2 dir = new Vector2((transform.position.x - col.transform.position.x > 0) ? 1 : -1, 0);
                Knockback(dir, knockbackScale);
                triggerframes = 0;
            }
        }
    }

}