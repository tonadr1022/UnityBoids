using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BoidSpawnSettings")]
public class BoidSpawnSettings : ScriptableObject
{
    public float spawnRadius = 10;
    public int spawnCount = 10;
}
