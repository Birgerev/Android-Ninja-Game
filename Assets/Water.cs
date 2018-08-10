using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject splash;
    public float yLevel;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.name.Contains("Kick"))
        {
            //Create Splash for any object falling into water
            GameObject obj = Instantiate(splash);
            obj.transform.position = new Vector2(col.transform.position.x, yLevel);

            if (col.gameObject.name.Contains("Ninja"))
            {
                //Destroy player and report to Manager that the game is over 
                Destroy(col.gameObject);
                Manager.instance.Lose();
            }
            else if(col.gameObject.name.Contains("Enemy"))
            {
                //Kill any enemies falling into the water
                col.GetComponent<Enemy>().Kill(false);
                //When game is over, no more score shall be added
                if (Manager.instance.spawnEnemies)
                    Manager.instance.score++;
            }
        }
    }
}
