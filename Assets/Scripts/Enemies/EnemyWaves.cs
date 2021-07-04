using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [Header("References to other scripts")]
    protected References references;
    protected SceneLoader sceneLoader;

    [Header("Spawning configuration")]
    public List<WaveConfig> waveConfigs;
    public float timeBetweenWaves = 4f;
    protected List<Transform> waypoints;
    private bool isLevelEndCheckStarted = false;

    protected virtual IEnumerator SpawnMultipleEnemies(int waveOrder, float spawnShift = 0, int kindOfDelay = -1, bool isFirstWave = false, bool isLastWave = false)
    {
        if (!isFirstWave) // Ускорение или отсрочка спавна волны при помощи блока свич
        {
            switch (kindOfDelay)
            {
                case -1:
                    yield return new WaitForSeconds(timeBetweenWaves * waveOrder);
                    break;
                case 0:
                    yield return new WaitForSeconds((timeBetweenWaves * waveOrder) - spawnShift);
                    break;
                case 1:
                    yield return new WaitForSeconds((timeBetweenWaves * waveOrder) + spawnShift);
                    break;
                default:
                    yield return new WaitForSeconds(timeBetweenWaves * waveOrder);
                    break;
            }
        }
        
        var waypoints = waveConfigs[waveOrder].GetWaypoints(0);
        var numberOfEnemies = waveConfigs[waveOrder].GetNumberOfEnemies();
        var enemyPrefabs = waveConfigs[waveOrder].GetEnemyPrefabs();

        GameObject[] enemies = new GameObject[numberOfEnemies];

        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (enemyPrefabs.Count == 1)
            {
                var singlePrefabEnemy = Instantiate
                    (waveConfigs[waveOrder].GetEnemyPrefabByIndex(0),
                    new Vector3(waypoints[i].position.x,
                    waypoints[i].position.y, -1),
                    Quaternion.identity);

                enemies[i] = singlePrefabEnemy;

                if (isLastWave == true && enemies[numberOfEnemies - 1] != null)
                {
                    StartCoroutine(InitiateLevelEnding(enemiesArray: enemies));
                }
            }

            else if(enemyPrefabs.Count > 1)
            {
                var enemy = Instantiate
                    (waveConfigs[waveOrder].GetEnemyPrefabByIndex(i),
                    new Vector3(waypoints[i].position.x,
                    waypoints[i].position.y, -1),
                    Quaternion.identity);

                enemies[i] = enemy;

                if (isLastWave == true && enemies[numberOfEnemies - 1] != null)
                {
                    StartCoroutine(InitiateLevelEnding(enemiesArray: enemies));
                }
            }

            yield return new WaitForSeconds(waveConfigs[waveOrder].GetTimeBetweenSpawns());
        }
    }

    protected virtual IEnumerator SpawnThreeSmallEnemies(int waveOrder, bool isFirstWave = false)
    {
        if (!isFirstWave)
        {
            yield return new WaitForSeconds(timeBetweenWaves * waveOrder);
        }

        var waypoints = waveConfigs[waveOrder].GetWaypoints(0);

        for (int i = 0; i < 2; i++)
        {
            Instantiate
                (waveConfigs[waveOrder].GetEnemyPrefabByIndex(0),
                waypoints[i].position,
                Quaternion.identity);
        }

        yield return new WaitForSeconds(waveConfigs[waveOrder].GetTimeBetweenSpawns()); 
        Instantiate(waveConfigs[waveOrder].GetEnemyPrefabByIndex(0), waypoints[2].position, Quaternion.identity);
    }

    protected virtual IEnumerator SpawnTwoFollowerTypeEnemies(int waveOrder, float spawnShift = 0)
    {
        yield return new WaitForSeconds((timeBetweenWaves * waveOrder) + spawnShift);

        var waypoints = waveConfigs[waveOrder].GetWaypoints(0);
        
        Instantiate
            (waveConfigs[waveOrder].GetEnemyPrefabByIndex(0),
            new Vector3(waypoints[1].position.x + 5f,
            waypoints[1].position.y + 5f, waypoints[1].position.z),
            Quaternion.identity);

        yield return new WaitForSeconds(waveConfigs[waveOrder].GetTimeBetweenSpawns());

        if (waveConfigs[waveOrder].GetNumberOfEnemies() == 2)
        {
            Instantiate
                (waveConfigs[waveOrder].GetEnemyPrefabByIndex(1),
                new Vector3(waypoints[1].position.x + 5f,
                waypoints[1].position.y + 5f,
                waypoints[1].position.z),
                Quaternion.identity);
        }
    }

    protected virtual IEnumerator SpawnSingleEnemy(int waveOrder, float spawnShift = 0, int kindOfDelay = -1, bool isFirstWave = false, bool isLastWave = false)
    {
        if (!isFirstWave)
        {
            switch (kindOfDelay)
            {
                case -1:
                    yield return new WaitForSeconds(timeBetweenWaves * waveOrder);
                    break;
                case 0:
                    yield return new WaitForSeconds((timeBetweenWaves * waveOrder) - spawnShift);
                    break;
                case 1:
                    yield return new WaitForSeconds((timeBetweenWaves * waveOrder) + spawnShift);
                    break;
                default:
                    yield return new WaitForSeconds(timeBetweenWaves * waveOrder);
                    break;
            }
        }

        var enemy = Instantiate
            (waveConfigs[waveOrder].GetEnemyPrefabByIndex(0),
            waveConfigs[waveOrder].GetWaypoints(0)[0].position,
            Quaternion.identity);

        GameObject[] enemies = new GameObject[1];
        enemies[0] = enemy;

        if (isLastWave == true)
        {
            Debug.Log("Initiating level end");
            StartCoroutine(InitiateLevelEnding(enemies));
        }
    }

    protected virtual IEnumerator InitiateLevelEnding(GameObject[] enemiesArray = null)
    {
        if(!isLevelEndCheckStarted)
        {
            isLevelEndCheckStarted = true;

            while (true)
            {
                if (enemiesArray.All(item => item == null))
                {
                    if (references != null && references.GetDialogueManagerTwo() != null)
                    {
                        references.GetDialogueManagerTwo().GetComponent<DialogueTrigger>().TriggerDialogue();
                        yield break;
                    }
                    else
                    {
                        StartCoroutine(sceneLoader.InitiateWinning());
                        yield break;
                    }
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}