using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public bool LockFpsSecure = true;
    public int target = 30;

    // Use this for initialization
    void Start () {

        if (LockFpsSecure)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;

    }
}
