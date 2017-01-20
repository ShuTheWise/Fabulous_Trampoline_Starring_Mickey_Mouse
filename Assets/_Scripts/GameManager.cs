using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class GameManager : Singleton<GameManager>
{
    public static bool CameraFree { get; set; }

    public GameObject[] firstPhase;
    public GameObject[] secondPhase;

    public GameObject[] end;
    private bool inSecondPhase;

    public bool InSecondPhase
    {
        get { return inSecondPhase; }
        set
        {
            inSecondPhase = value;
            //  GameObject.FindGameObjectWithTag("Player").SetActive(value);
            foreach (var item in FindObjectsOfType<EnviormentTrampolineBehaviour>())
            {
                item.enabled = value;
            }
            foreach (var dr in FindObjectsOfType<AddRb>())
            {
                dr.enabled = value;
            }
            foreach (var dr in FindObjectsOfType<DragTrampoline>())
            {
                dr.enabled = !value;
            }
            foreach (var item in secondPhase)
            {
                item.SetActive(value);
            }
            foreach (var item in firstPhase)
            {
                item.SetActive(!value);
            }
            Camera.main.GetComponent<MainCameraScript>().CamBeh = value ? CameraBehaviour.Follow : CameraBehaviour.Swype;

        }
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        CameraFree = true;
        InSecondPhase = false;
    }

    // Update is called once per frame
    public void ReloadMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void End()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
    //    p.GetComponent<PlatformerCharacter2D>().enabled = false;
        p.GetComponent<Platformer2DUserControl>().enabled = false;

        foreach (var item in secondPhase)
        {
            item.SetActive(false);
        }
        foreach (var item in firstPhase)
        {
            item.SetActive(false);
        }
        foreach (var VARIABLE in end)
        {
            VARIABLE.SetActive(true);
        }
        p.SetActive(true);
     
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
