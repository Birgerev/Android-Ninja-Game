using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreCharacter : MonoBehaviour {

    public Sprite[] character;
    public Sprite empty;
    public Image sprite;

    int index;

	// Use this for initialization
	void Start () {
        index = transform.GetSiblingIndex();
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            sprite.sprite = character[int.Parse(Manager.instance.score.ToString()[index].ToString())];
        }catch(Exception e)
        {
            sprite.sprite = empty;
        }
    }
}
