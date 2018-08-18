using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Vector3 direction;
    public GameObject owner;

    public float cooldown;
    public int maxAmount;
    public float recharge;
    public float damage;

    public int despawnAge = 75;

    int age;
    
	void Update () {
        age++;
        if (age > despawnAge)
            Destroy (gameObject);
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (owner != col.gameObject)
        {
            CameraManager.instance.Shake();

            if (col.GetComponent<Enemy>() != null)
            {
                col.GetComponent<Enemy>().Knockback(direction, 
                    new Vector2(col.GetComponent<Enemy>().knockbackScale, col.GetComponent<Enemy>().knockbackScale));
                col.GetComponent<Enemy>().Damage(damage);

                Destroy(gameObject);
            }
            else if (col.GetComponent<Player>() != null)
            {
                //TODO make player knock over
                Destroy(gameObject);
            }
        }
    }
}
