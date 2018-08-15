using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public float loopSpeed;
    public GameObject[] spawnpoints;

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
                //Spawn Logic

                GameObject spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];

                Vector2 direction = new Vector2((spawnpoint.transform.position.x < 0) ? 1 : -1, 0);

                GameObject obj = Instantiate(enemy);
                obj.transform.position = spawnpoint.transform.position;
                obj.GetComponent<Enemy>().forward = direction;

                //Wait For Next Iteration
                yield return new WaitForSeconds(loopSpeed);
            }
            else yield return new WaitForSeconds(0.1f);    //As To Not Crash The Game
        }
    }
}
