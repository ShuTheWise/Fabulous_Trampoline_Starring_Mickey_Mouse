using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragRigidbody2D : MonoBehaviour
{
    private Collider2D _col;
    private Rigidbody2D _rb;
    private Camera _mainCamera;
   

    private static Vector3 InputVector
    {
        //TODO: Change for android build
        get
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            return DragCondition ? (Vector3) Input.GetTouch(0).position : Vector3.zero;
#endif
        }
    }

    private static bool DragCondition
    {

        //TODO: Change for android build
        get
        {
#if UNITY_EDITOR
            return Input.GetButton("Fire1");
#else
            return Input.touchCount == 1;
#endif
        }

    }


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _col = GetComponent<Collider2D>();
    }

    void Update()
    {
        Ray one = _mainCamera.ViewportPointToRay(InputVector);
        RaycastHit2D hitOne = Physics2D.GetRayIntersection(one, Mathf.Infinity);

        Ray ray = _mainCamera.ScreenPointToRay(InputVector);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (!hit.rigidbody) return;
        if (hit.rigidbody != _rb) return;
        StartCoroutine(DragObject());

    }
    IEnumerator DragObject()
    {
        _col.isTrigger = true;
        GameManager.CameraFree = false;
       // GameManager.Holding = true;
        while (DragCondition && !GameManager.CameraFree)
        {
            Ray ray = _mainCamera.ScreenPointToRay(InputVector);
            gameObject.transform.position = new Vector3(ray.origin.x, ray.origin.y, 0);
            yield return null;
        }
        _col.isTrigger = false;
        GameManager.CameraFree = true;
    }
    
}