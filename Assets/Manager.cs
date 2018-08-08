using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public bool LockFpsSecure = true;
    public int targetFps = 30;

    public int score = 30;
    public GameObject PlayerPrefab;

    public bool spawnEnemies = false;

    public static Manager instance;

    // Use this for initialization
    void Start () {
        instance = this;
        if (LockFpsSecure)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }

        //For Testing
        Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Application.targetFrameRate != targetFps)
            Application.targetFrameRate = targetFps;

    }

    public void Lose()
    {
        spawnEnemies = false;
    }

    public void Reset()
    {

    }

    public void Play()
    {
        Instantiate(PlayerPrefab);
    }
}
