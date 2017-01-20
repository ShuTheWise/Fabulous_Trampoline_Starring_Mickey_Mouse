using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("1_Forest"));

    }

}
