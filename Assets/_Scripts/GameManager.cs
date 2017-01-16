using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	// Use this for initialization
    public GameObject startPoint;
    public GameObject finishPoint;

    public static bool CameraFree { get; set; }
 //   public static bool Holding { get; set; }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        CameraFree = true;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
