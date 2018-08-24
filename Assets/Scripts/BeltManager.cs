using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltManager : MonoBehaviour {

    public static Color color;

    public Color[] colors;
    public int[] highscoreForUnlock;

    int Selected = 0;
    public int selectedColor
    {                   //saved between sessions.
        get
        {
            return Selected;
        }

        set
        {
            PlayerPrefs.SetInt("selectedBelt", value);
            Selected = value;
        }
    }

    public static BeltManager instance;

    // Use this for initialization
    void Start () {
        instance = this;
        Selected = PlayerPrefs.GetInt("selectedBelt");
    }
	
	// Update is called once per frame
	void Update ()
    {
        color = colors[selectedColor];

    }
}
