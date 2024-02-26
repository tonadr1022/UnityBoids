using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/BoidSettings")]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float perceptionRadius = 2.5f;
    public float separationRadius = 1;
    public float maxSteerForce = 3;
    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float separateWeight = 1;
    public float targetWeight = 1;
    
}