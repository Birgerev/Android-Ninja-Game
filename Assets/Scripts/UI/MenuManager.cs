using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        anim.SetTrigger("Next");

        Manager.instance.Play();
    }

    public void Lose()
    {
        //Called by Manager
        anim.SetTrigger("Next");
    }

    public void Menu()
    {
        //Called by Invinsible Button In 'lose' ui Section
        anim.SetTrigger("Next");

        Manager.instance.Reset();
    }

    public void Shop()
    {
        //Called by Button In 'menu' ui Section
        anim.SetTrigger("Secondary");
    }

    public void Respawn()
    {
        //Called by Manager on Random
        anim.SetTrigger("Secondary");
    }

    public void RespawnToGame()
    {
        //Called by Manager
        anim.SetTrigger("Secondary");
    }

    public void WatchAd()
    {
        //Called by UI button 'Accept'
        AdManager.instance.playAd();
    }

    public void CancelRespawn()
    {
        //Called by UI button 'Cancel'
        anim.SetTrigger("Next");
    }
}
