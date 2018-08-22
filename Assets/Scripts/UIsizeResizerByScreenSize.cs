using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsizeResizerByScreenSize : MonoBehaviour {

    public float myX = 1920;
    public float myY = 1080;

    public float thierX;
    public float thierY;

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        thierX = Screen.width;
        thierY = Screen.height;

        print(thierX + " . " + thierY);
        print(myX + " / " + myY);

        print("this is: " + thierX / myX);
        float percentageX = thierX / myX;
        //float sizeX = ((percentageX / 10) * myX) / 100;


        float percentageY = thierY / myY;
        //float sizeY = ((percentageY / 10) * myY) / 100;

        print(percentageX + " : " + percentageY);

        transform.localScale = new Vector2(percentageX, percentageY);
    }
}
