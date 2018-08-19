using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltManager : MonoBehaviour {

    public static Color color;

    public Color[] colors;
    public int selectedColor;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        color = colors[selectedColor];

    }
}
