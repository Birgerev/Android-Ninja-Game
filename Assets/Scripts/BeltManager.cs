using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltManager : MonoBehaviour {

    public static Color color;

    public Color[] colors;
    public int[] highscoreForUnlock;
    public int selectedColor;

    public static BeltManager instance;

    // Use this for initialization
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update ()
    {
        color = colors[selectedColor];

    }
}
