using UnityEngine;
using System;

public class TapTrigger : MonoBehaviour {

    public Vector2 dir;

    void OnMouseUp()
    {
        //When Player Doesn't Exist this will throw an error
        try
        {
            GameObject.Find("Ninja").GetComponent<Player>().Throw(dir);
        }
        catch (Exception e)
        {

        }
    }
}
