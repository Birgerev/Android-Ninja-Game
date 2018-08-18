using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

    public Vector2 strength;

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null && !enemy.GetComponent<RigidbodyPixel>().doGravity)
        {
            RigidbodyPixel rb = enemy.GetComponent<RigidbodyPixel>();
            rb.doGravity = true;
            rb.velocity = new Vector2((transform.position.x < 0) ? 1 : -1, 1) * strength;
        }
    }
}
