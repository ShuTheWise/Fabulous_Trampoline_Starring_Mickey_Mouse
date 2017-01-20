using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    public float X;
    public float Y;
    private float _cxMin;
    private float _cyMin;
    private float _cxMax;
    private float _cyMax;

    private void Start()
    {
        var _aspect = Camera.main.aspect;
        var size = Camera.main.orthographicSize;
        _cxMin = 2 * size * _aspect / 2;
        _cyMin = 2 * size / 2;
        _cxMax = X - _cxMin;
        _cyMax = Y - _cyMin;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _cxMin, _cxMax), Mathf.Clamp(transform.position.y, _cyMin, _cyMax), -10f);
    }
   
}
