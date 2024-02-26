using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Boid : MonoBehaviour
{
    [HideInInspector] public Vector3 position;
    [HideInInspector] public float angle;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 velocity;


    [HideInInspector] public Vector3 alignmentDir;
    [HideInInspector] public Vector3 separationDir;
    [HideInInspector] public Vector3 cohesionDir;
    [HideInInspector] public Vector3 acceleration;

    private SpriteRenderer _renderer;
    private BoidSettings _settings;
    private BoidGizmoSettings _gizmoSettings;
    private Boundary _boundary;
    private Transform _cachedTransform;
    private Transform _target;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _cachedTransform = transform;
    }

    public void Initialize(BoidSettings settings, BoidGizmoSettings gizmoSettings, Boundary boundary)
    {
        _gizmoSettings = gizmoSettings;
        _settings = settings;
        _boundary = boundary;
        position = _cachedTransform.position;
        velocity = Random.insideUnitCircle * ((settings.maxSpeed + settings.minSpeed) / 2);
    }

    public void UpdatePhysics()
    {
        velocity += acceleration * Time.deltaTime;
        var speed = velocity.magnitude;
        velocity = velocity.normalized * Mathf.Clamp(speed, _settings.minSpeed, _settings.maxSpeed);
        direction = velocity.normalized;
        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;

        _cachedTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        position += velocity * Time.deltaTime;
        WrapBoundary();
        _cachedTransform.position = position;
    }

    private void WrapBoundary()
    {
        if (position.x > _boundary.XMax)
        {
            position.x -= _boundary.XRange;
        }
        else if (position.x < _boundary.XMin)
        {
            position.x += _boundary.XRange;
        }

        if (position.y > _boundary.YMax)
        {
            position.y -= _boundary.YRange;
        }
        else if (position.y < _boundary.YMin)
        {
            position.y += _boundary.YRange;
        }
    }

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }

    public void OnDrawGizmos()
    {
        if (_gizmoSettings.acceleration && acceleration != Vector3.zero)
        {
            Gizmos.color = _gizmoSettings.accelerationColor;
            Gizmos.DrawLine(position, position + acceleration);
        }

        if (_gizmoSettings.separationDir && separationDir != Vector3.zero)
        {
            Gizmos.color = _gizmoSettings.separationDirColor;
            Gizmos.DrawLine(position, position + separationDir);
        }

        if (_gizmoSettings.cohesionDir && cohesionDir != Vector3.zero)
        {
            Gizmos.color = _gizmoSettings.cohesionDirColor;
            Gizmos.DrawLine(position, position + cohesionDir);
        }

        if (_gizmoSettings.alignmentDir && alignmentDir != Vector3.zero)
        {
            Gizmos.color = _gizmoSettings.alignmentDirColor;
            Gizmos.DrawLine(position, position + alignmentDir);
        }

        if (_gizmoSettings.perceptionRadius)
        {
            Gizmos.color = _gizmoSettings.perceptionRadiusColor;
            Gizmos.DrawWireSphere(position, _settings.perceptionRadius);
        }

        if (_gizmoSettings.separationRadius)
        {
            Gizmos.color = _gizmoSettings.separationRadiusColor;
            Gizmos.DrawWireSphere(position, _settings.separationRadius);
        }
    }
}