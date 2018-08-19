using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    public int animationId = 0;
    public float maxAge = 2;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        anim.SetInteger("animation", animationId);

        StartCoroutine(kill());
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(maxAge);
        //Destroy object
        Destroy(gameObject);
    }
}
