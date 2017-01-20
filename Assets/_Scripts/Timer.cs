using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
    private TimeSpan ts;
    private DateTime start;

    private void Start()
    {
        start = DateTime.Now;
        InvokeRepeating("Run",0,0.1f);
    }

    void Run()
    {
        ts = DateTime.Now - start;

        GetComponent<Text>().text = string.Format("{1:00}:{0:00}:{2:00}", ts.Seconds, ts.Minutes, ts.Milliseconds);
       //f.Duration()
    }

    public TimeSpan Stop()
    {
        CancelInvoke();
        return ts;
    }
}
