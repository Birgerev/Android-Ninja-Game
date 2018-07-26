using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour {

    public Vector2 dir;
    
    void OnMouseDown()
    {
        GameObject.Find("Ninja").GetComponent<Player>().Throw(dir);
    }
}
