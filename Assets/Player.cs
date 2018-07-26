using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Manager manager;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Dash(new Vector2(-1, 0));
        if (Input.GetKeyDown(KeyCode.D))
            Dash(new Vector2(-1, 0));

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public void Dash(Vector2 dir)
    {

    }

    public void Jump()
    {

    }

    public void Throw(Vector2 dir)
    {

    }

    public void Die()
    {

    }
}