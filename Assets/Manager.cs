using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public bool LockFpsSecure = true;
    public int targetFps = 30;

    public int score = 30;
    public GameObject PlayerPrefab;

    public MenuManager UI;

    public bool spawnEnemies = false;
    public bool controllPlayer = false;

    public static Manager instance;

    // Use this for initialization
    void Start () {
        instance = this;
        UI = GameObject.Find("UI").GetComponent<MenuManager>();

        if (LockFpsSecure)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }

        Reset();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Application.targetFrameRate != targetFps)
            Application.targetFrameRate = targetFps;

    }

    public void Lose()
    {
        Manager.instance.controllPlayer = false;
        spawnEnemies = false;
        UI.Lose();
    }

    public void Reset()
    {
        score = 0;
        
        Instantiate(PlayerPrefab);
        
        KillEnemies();
    }

    public void Play()
    {
        Manager.instance.controllPlayer = true;
        spawnEnemies = true;
    }

    public void KillEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().Kill(true);
        }
    }
}
