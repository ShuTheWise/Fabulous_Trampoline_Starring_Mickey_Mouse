using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public enum CameraBehaviour
{
    Swype,
    Follow
};
public class MainCameraScript : MonoBehaviour
{

    public float Speed = 0.1F;
    private const float _speedEditor = 1f;

    [SerializeField]
    private CameraBehaviour camBeh;
    public CameraBehaviour CamBeh
    {
        get
        {
            return camBeh;
        }
        set
        {
            camBeh = value;
            if (camBeh == CameraBehaviour.Swype)
            {
                GetComponent<Camera2DFollow>().enabled = false;
                GetComponent<MainCameraScript>().enabled = true;
            }
            else
            {
                GetComponent<Camera2DFollow>().enabled = true;
                GetComponent<MainCameraScript>().enabled = false;
            }
        }
    }

    private void Start()
    {
        CamBeh = camBeh;
    }

    void Update()
    {
        if (GameManager.CameraFree)
        {
#if UNITY_EDITOR
            var inputx = Input.GetAxis("Horizontal");
            var inputy = Input.GetAxis("Vertical");
            if (inputy > .5f || inputy < -.5f)
            {
                transform.Translate(0, inputy*_speedEditor, 0);
            }
            if (inputx > .5f || inputx < -.5f)
            {
                transform.Translate(inputx*_speedEditor, 0, 0);
            }


#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);


        }
#endif
        }
    }
}