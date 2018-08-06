using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpacer : MonoBehaviour {

    float space;

	// Use this for initialization
	void Start () {
        space = transform.GetChild(transform.childCount - 1).localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.instance.score > 99)
        {
            transform.localPosition = new Vector2(0, transform.localPosition.y);
        }else if (Manager.instance.score > 9)
        {
            transform.localPosition = new Vector2((space), transform.localPosition.y);
        }else
        {
            transform.localPosition = new Vector2((space*1.5f), transform.localPosition.y);
        }

    }
}
