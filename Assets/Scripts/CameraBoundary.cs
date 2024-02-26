using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public Boundary boundary;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        boundary.SetBoundary(_camera.ViewportToWorldPoint(Vector2.zero), _camera.ViewportToWorldPoint(Vector2.one)); 
         Debug.Log(boundary.XMin + " " + boundary.XMax + " " + boundary.YMin + " " + boundary.YMax);
    }
    
}
