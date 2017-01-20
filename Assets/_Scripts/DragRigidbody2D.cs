using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragRigidbody2D : MonoBehaviour
{
    private Collider2D _col;
    private Rigidbody2D _rb;
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
        Dragging = true;
        StartCoroutine(DragObject(hit.rigidbody));


    }
    IEnumerator DragObject(Rigidbody2D rb)
    {
        // _col.isTrigger = true;
        GameManager.CameraFree = false;
        var allThis = FindObjectsOfType<DragRigidbody2D>();
        var allTramps = FindObjectsOfType<DragTrampoline>();
        
        foreach (var item in allThis)
        {
            if (item != this)
            {
                item.enabled = false;
            }
        }
        foreach (var v in allTramps)
        {

            v.enabled = false;

        }
        //  pt.transform.localScale = new Vector2(max,max);
        while (DragCondition && !GameManager.CameraFree)
        {
            Ray ray = _mainCamera.ScreenPointToRay(InputVector);
            gameObject.transform.position = new Vector3(ray.origin.x, ray.origin.y, 0);
            rb.simulated = false;
            yield return null;

        }
        DestroyImmediate(pt);
        //rb.simulated = true;
        //  _col.isTrigger = false;
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