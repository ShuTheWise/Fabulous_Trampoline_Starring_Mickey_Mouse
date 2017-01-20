using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragTrampoline : MonoBehaviour
{
   public Collider2D DragCollider;
    private Camera _mainCamera;
    private bool _dragging;
    private GameObject pt;


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

    public bool Dragging
    {
        get
        {
            return _dragging;
        }
        set
        {
            _dragging = value;
            if (value)
            {
                if (pt != null) return;
                pt = Instantiate(Resources.Load("pt")) as GameObject;
                pt.transform.position = transform.position;
                pt.transform.parent = transform;

            }
            else
            {
                DestroyImmediate(pt);
            }

        }
    }
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Ray one = _mainCamera.ViewportPointToRay(InputVector);
        RaycastHit2D hitOne = Physics2D.GetRayIntersection(one, Mathf.Infinity);

        Ray ray = _mainCamera.ScreenPointToRay(InputVector);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (!hit.collider) return;
        if (hit.collider != DragCollider) return;
        Dragging = true;
        StartCoroutine(DragObject());


    }
    IEnumerator DragObject()
    {
        GameManager.CameraFree = false;
        var allThis = FindObjectsOfType<DragRigidbody2D>();
        var allTramps = FindObjectsOfType<DragTrampoline>();
        foreach (var item in allThis)
        {
           
                item.enabled = false;
            
        }
        foreach (var v in allTramps)
        {
            if (v != this)
            {

                v.enabled = false;
            }

        }
        while (DragCondition && !GameManager.CameraFree)
        {
            Ray ray = _mainCamera.ScreenPointToRay(InputVector);
            gameObject.transform.position = new Vector3(ray.origin.x, ray.origin.y, 0);
          
            yield return null;
        }
        DestroyImmediate(pt);
        GameManager.CameraFree = true;
        Dragging = false;
        foreach (var item in allThis)
        {
            item.enabled = true;
        }
        foreach (var v in allTramps)
        {

            v.enabled = true;

        }
    }

}