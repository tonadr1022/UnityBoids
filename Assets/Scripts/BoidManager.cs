using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BoidManager : MonoBehaviour
{
    private Boid[] _boids;
    public BoidSettings settings;
    public BoidGizmoSettings boidGizmoSettings;
    public Boundary boundary;

    public void Start()
    {
        _boids = FindObjectsOfType<Boid>();
        foreach (var b in _boids)
        {
            b.Initialize(settings,boidGizmoSettings, boundary);
        }
    }

    public void Update()
    {
        foreach (var b in _boids)
        {
            UpdateBoid(b);
        }
    }

    private void UpdateBoid(Boid boid)
    {

        var numNeighborBoids = 0;
        var alignmentDir = Vector3.zero;
        var flockCenterDir = Vector3.zero;
        var separationDir = Vector3.zero;
        var sqPerceptionRadius = settings.perceptionRadius * settings.perceptionRadius;
        var sqAvoidanceRadius = settings.separationRadius * settings.separationRadius;

        foreach (var otherBoid in _boids)
        {
            if (boid == otherBoid)
            {
                continue;
            }

            var offset = otherBoid.position - boid.position;
            var sqDist = offset.x * offset.x + offset.y * offset.y;

            if (!(sqDist < sqPerceptionRadius)) continue;
            numNeighborBoids++;
            alignmentDir += otherBoid.direction;
            flockCenterDir += otherBoid.position;

            if (sqDist < sqAvoidanceRadius)
            {
                separationDir -= offset / sqDist;
            }
        }

        var acceleration = Vector3.zero;
        var cohesionDirection = Vector3.zero;
        if (numNeighborBoids > 0)
        {
            flockCenterDir /= numNeighborBoids;

            cohesionDirection = flockCenterDir - boid.position;
            var cohesionForce = DirectionToForce(cohesionDirection, boid.velocity) * settings.cohesionWeight;
            var alignmentForce = DirectionToForce(alignmentDir, boid.velocity) * settings.alignWeight;
            var separationForce = DirectionToForce(separationDir, boid.velocity) * settings.separateWeight;
            acceleration += cohesionForce + alignmentForce + separationForce;
        }

        boid.acceleration = acceleration;
        boid.separationDir = separationDir;
        boid.alignmentDir = alignmentDir;
        boid.cohesionDir = cohesionDirection;
        
        boid.UpdatePhysics();
    }

    private Vector3 DirectionToForce(Vector3 direction, Vector3 currVelocity)
    {
        var v = direction.normalized * settings.maxSpeed - currVelocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }  
}



