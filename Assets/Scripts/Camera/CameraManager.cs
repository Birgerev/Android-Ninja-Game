using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject player;
    public static CameraManager instance;

    public bool lockX;
    public bool lockY;

    public float maxRoll = 10;
    public float maxOffset = 0.5f;
    public float shake = 0;
    public float shakedropoffPerFrame = 0.8f;
    public float shakeforce = 1.5f;

    public GameObject flash;

    public float roll;
    public float offsetX;
    public float offsetY;

    int seed = 0;

    // Use this for initialization
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            player = GameObject.Find("Ninja");

        if(player != null)
        {
            ShakeCalc();

            Vector2 pos = player.transform.position;
            float angle = 0;
            if (!lockX)
                pos = new Vector2(0, pos.y);
            if (!lockY)
                pos = new Vector2(pos.x, 0);

            pos += new Vector2(offsetX, offsetY);
            angle = roll;

            GetComponent<RigidbodyPixel>().position = pos;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    public void Flash()
    {

        flash.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.8f);
        instance.StartCoroutine(FadeFlash());

    }

    public IEnumerator FadeFlash()
    {
        yield return new WaitForSeconds(0.05f);
        flash.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }

    void ShakeCalc()
    {
        //Seeds needs to use fractal numbers
        roll = maxRoll * shake * Mathf.PerlinNoise(seed + 0.4f, seed + 0.4f);
        offsetX = maxOffset * shake * Mathf.PerlinNoise(seed+1.4f, seed+1.4f);
        offsetY = maxOffset * shake * Mathf.PerlinNoise(seed+2.4f, seed+2.4f);
        
        seed+=1;

        shake *= shakedropoffPerFrame;
        if (shake < 0.001f)
            shake = 0;
    }

    public void Shake()
    {
        shake += shakeforce;
    }
}
