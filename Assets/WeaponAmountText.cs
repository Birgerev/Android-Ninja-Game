using UnityEngine.UI;
using UnityEngine;

public class WeaponAmountText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<Text>().text = GameObject.Find("Ninja").GetComponent<Player>().weaponAmount.ToString();
	}
}
