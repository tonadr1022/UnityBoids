using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/BoidGizmoSettings")]
public class BoidGizmoSettings : ScriptableObject
{
    public bool perceptionRadius = false;
    public Color perceptionRadiusColor = Color.green;
    public bool separationRadius = false;
    public Color separationRadiusColor = Color.red;
    public bool acceleration = false;
    public Color accelerationColor = Color.cyan;
    public bool alignmentDir = false;
    public Color alignmentDirColor = Color.magenta;
    public bool separationDir = false;
    public Color separationDirColor = Color.yellow;
    public bool cohesionDir = false;
    public Color cohesionDirColor = Color.white;
}
