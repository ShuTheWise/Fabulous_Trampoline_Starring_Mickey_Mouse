using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{

    public float Speed = 0.1F;
    private const float _speedEditor =1f;


    void Update()
    {
        if (GameManager.CameraFree)
        {
#if UNITY_EDITOR
            var inputx = Input.GetAxis("Horizontal");
            var inputy = Input.GetAxis("Vertical");
            if (inputy > .5f || inputy < -.5f)
            {
                transform.Translate(0, inputy * _speedEditor, 0);
            }
            if (inputx > .5f || inputx < -.5f)
            {
                transform.Translate(inputx * _speedEditor, 0, 0);
            }


#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);
#endif

        }
    }
}