using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BeltButton : MonoBehaviour {

    public Text Description;
    public Image Belt;
    public GameObject Lock;
    public GameObject Selected;

    private int index;

    // Use this for initialization
    void Start () {
        index = transform.GetSiblingIndex();

    }
	
	// Update is called once per frame
	void Update () {
        Belt.color = BeltManager.instance.colors[index];
        Description.text = "Highscore "+(Manager.instance.highscore + "/" + BeltManager.instance.highscoreForUnlock[index]);

        Lock.SetActive(Manager.instance.highscore < BeltManager.instance.highscoreForUnlock[index]);
        Selected.SetActive(BeltManager.instance.selectedColor == index);
    }

    public void Click()
    {
        if(Manager.instance.highscore >= BeltManager.instance.highscoreForUnlock[index])
            BeltManager.instance.selectedColor = index;
    }
}
