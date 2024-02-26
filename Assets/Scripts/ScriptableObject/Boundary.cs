using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{ 
    public float XMin { get; private set; }
    public float XMax { get; private set; }
    public float YMin { get; private set; }
    public float YMax { get; private set; }
    
    public float XRange { get; private set; }
    public float YRange { get; private set; }
    public void SetBoundary(Vector2 min, Vector2 max)
    {
        XMin = min.x;
        XMax = max.x;
        YMin = min.y;
        YMax = max.y;
        XRange = max.x - min.x;
        YRange = max.y - min.y;
    }
    
    public Vector2 RandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(XMin, XMax), UnityEngine.Random.Range(YMin, YMax));
    }
}
