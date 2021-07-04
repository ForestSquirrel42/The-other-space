using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<GameObject> pathPrefabs;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] int numberOfEnemies;

    public GameObject GetEnemyPrefabByIndex(int index) { return enemyPrefabs[index]; }
    public List<GameObject> GetEnemyPrefabs() { return enemyPrefabs; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public List<Transform> GetWaypoints(int index)
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefabs[index].transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
}
