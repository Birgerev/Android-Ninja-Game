using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public bool LockFpsSecure = true;
    public int targetFps = 30;
    
    public int coins    //Use PlayerPrefs As A Variable Which also means coins value will be 
    {                   //saved between sessions.
        get
        {
            return PlayerPrefs.GetInt("coins", 0);
        }
        set
        {
            PlayerPrefs.SetInt("coins", value);
        }
    }

    public int score = 30;
    public int highscore = 30;
    public GameObject PlayerPrefab;
    public GameObject effectPrefab;

    public MenuManager UI;

    public bool spawnEnemies = false;
    public bool controllPlayer = false;
    public bool resetStats = false;

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

        highscore = PlayerPrefs.GetInt("highscore", 0);

        Reset();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (resetStats)
        {
            PlayerPrefs.DeleteAll();
            resetStats = false;
        }

        if (Application.targetFrameRate != targetFps)
            Application.targetFrameRate = targetFps;

        //Update Highscore
        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
        }

    }

    public void Respawn()
    {
        //Called By Admanager after an ad has been watched
        Instantiate(PlayerPrefab);
        KillEnemies();

        Manager.instance.controllPlayer = true;
        spawnEnemies = true;

        UI.RespawnToGame();
    }

    public void Lose()
    {
        Manager.instance.controllPlayer = false;
        spawnEnemies = false;
        //Will Throw An Error On PC        
        if (Manager.instance.score > 20)
        {
            UI.Respawn();
            return;
        }

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

        AdManager.instance.loadAd();
    }

    public void KillEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().Kill(true);
        }
    }

    public void Effect(int id, Vector2 pos)
    {
        GameObject obj = Instantiate(effectPrefab);

        obj.transform.position = pos;
        obj.GetComponent<Effect>().animationId = id;
    }
}
