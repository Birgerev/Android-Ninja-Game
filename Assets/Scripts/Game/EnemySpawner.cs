using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject[] spawnpoints;

    public Dictionary<int, int> levelsatscore = new Dictionary<int, int>()
    {
        { 0, 0},
        { 1, 20},
    };

    public Dictionary<float, int> speedatscore = new Dictionary<float, int>()
    {
        { 1.5f, 0},
        { 1.2f, 20},
        { 1f, 50},
        { 0.8f, 100},
    };

    // Use this for initialization
    void Start () {
        StartCoroutine(loop());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator loop()
    {
        while (true)
        {
            if (Manager.instance.spawnEnemies)
            {
                int maxlevel = 0;
                foreach (KeyValuePair<int, int> entry in levelsatscore)
                {
                    if (entry.Value <= Manager.instance.score)
                    {
                        maxlevel = entry.Key;
                    }
                }
                //Spawn Logic
                int level = Random.Range(0, maxlevel+1);

                GameObject spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];

                Vector2 direction = new Vector2((spawnpoint.transform.position.x < 0) ? 1 : -1, 0);

                GameObject obj = Instantiate(enemy);
                obj.transform.position = spawnpoint.transform.position;
                obj.GetComponent<Enemy>().forward = direction;
                obj.GetComponent<Enemy>().enemyLevel = level;

                //Wait For Next Iteration
                float loopSpeed = 2;
                foreach (KeyValuePair<float, int> entry in speedatscore)
                {
                    if (entry.Value <= Manager.instance.score)
                    {
                        loopSpeed = entry.Key;
                    }
                }
                yield return new WaitForSeconds(loopSpeed);
            }
            else yield return new WaitForSeconds(0.1f);    //As To Not Crash The Game
        }
    }
}
