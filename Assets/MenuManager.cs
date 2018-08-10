using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject menu;
    public GameObject game;
    public GameObject lose;

    // Use this for initialization
    void Start () {
        //Menu();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        //Called by Invinsible Button In 'game' ui Section
        menu.SetActive(false);
        game.SetActive(true);

        Manager.instance.Play();
    }

    public void Lose()
    {
        //Called by Manager
        game.SetActive(false);
        lose.SetActive(true);
    }

    public void Menu()
    {
        //Called by Invinsible Button In 'lose' ui Section
        lose.SetActive(false);
        menu.SetActive(true);

        Manager.instance.Reset();
    }
}
