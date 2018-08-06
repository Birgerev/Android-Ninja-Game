using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public bool LockFpsSecure = true;
    public int target = 30;

    public int score = 30;

    public static Manager instance;

    // Use this for initialization
    void Start () {
        instance = this;
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
