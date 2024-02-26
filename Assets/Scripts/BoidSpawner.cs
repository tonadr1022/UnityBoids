using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoidSpawner : MonoBehaviour
{
    public enum GizmoType
    {
        Never,
        OnSelected,
        Always
    }

    public Boid prefab;
    public Color color;
    
    public GizmoType showSpawnRegion;
    [SerializeField] private BoidSpawnSettings boidSpawnSettings;
    


    public void Awake()
    {
        SpawnBoids();
    }

    public void SpawnBoids()
    {
        for (var i = 0; i < boidSpawnSettings.spawnCount; i++)
        {
            var boid = Instantiate(prefab);
            var boidTransform = boid.transform;
            var pos = Random.insideUnitCircle * boidSpawnSettings.spawnRadius;
            boidTransform.position = transform.position + new Vector3(pos.x, pos.y, 0f);
            boid.SetColor(color);
        }
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showSpawnRegion == GizmoType.OnSelected)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {
        Gizmos.color = new Color(color.r,color.g,color.b,0.3f);
        Gizmos.DrawSphere(transform.position, boidSpawnSettings.spawnRadius);
    }
}