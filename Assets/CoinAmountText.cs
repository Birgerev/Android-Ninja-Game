using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAmountText : MonoBehaviour {
    
	void OnEnable ()
    {
        StartCoroutine(loop());
	}

    IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GetComponent<Text>().text = "" + Manager.instance.coins;
            print(Manager.instance.coins);
        }
    }
}
