using UnityEngine.UI;
using UnityEngine;

public class HighscoreText : MonoBehaviour {

    public string prefix, suffix;
    
	void Update () {
        GetComponent<Text>().text = prefix + Manager.instance.highscore + suffix;
	}
}
