using UnityEngine.UI;
using UnityEngine;
using System;

public class WeaponAmountText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        try {
            transform.GetComponent<Text>().text = GameObject.Find("Ninja").GetComponent<Player>().weaponAmount.ToString();
        }
        catch (Exception e)
        {

        }
    }
}
