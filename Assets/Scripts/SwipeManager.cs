using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour {

    public float minRadius = 100f;

    public Player player;
    public Vector3 lastTouch;
    
	void Start () {
        player = GetComponent<Player>();

    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            lastTouch = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 deltaTouch = lastTouch - Input.mousePosition;
            Vector2 result = Vector2.zero;

            lastTouch = Vector3.zero;
            
            //results are inverted
            if (deltaTouch.x > minRadius)
                result = new Vector2(-1, 0);
            if (deltaTouch.x < -minRadius)
                result = new Vector2(1, 0);
            if (deltaTouch.y > minRadius)
                result = new Vector2(0, -1);
            if (deltaTouch.y < -minRadius)
                result = new Vector2(0, 1);

            if (result == Vector2.zero)
            {
                player.Throw();
            }else
            {
                Swipe(result);
            }
        }
    }

    public void Swipe(Vector3 dir)
    {
        if(dir.y == 0)
            player.Dash(dir);
        else if(dir.y == 1)
            player.Jump();
    }
}
