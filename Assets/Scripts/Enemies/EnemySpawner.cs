using System.Collections;
using UnityEngine;

public class EnemySpawner : EnemyWaves
{
    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Start()
    {
        switch(SceneLoader.GetCurrentSceneByNameStatic())
        {
            case "Level 1-1":
                StartCoroutine(SpawnWavesLvlOneOne());
                break;
            case "Level 1-2":
                StartCoroutine(SpawnWavesLvlOneTwo());
                break;
            case "Level 1-3":
                StartCoroutine(SpawnWavesLvlOneThree());
                break;
            case "Level 1-4":
                StartCoroutine(SpawnWavesLvlOneFour());
                break;
            case "Level 1-5":
                StartCoroutine(SpawnWavesLvlOneFive());
                break;
            case "Level 1-6":
                StartCoroutine(SpawnWavesLvlOneSix());
                break;
            case "Level 1-7":
                StartCoroutine(SpawnWavesLvlOneSeven());
                break;
        }
    }

    private IEnumerator SpawnWavesLvlOneOne()
    {
        if(PlayerMovement.playerHasControl == true)
        {
            StartCoroutine(SpawnThreeSmallEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnTwoFollowerTypeEnemies(waveOrder : 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder : 2));

            StartCoroutine(SpawnMultipleEnemies(waveOrder : 3));

            StartCoroutine(SpawnMultipleEnemies(waveOrder : 4, timeBetweenWaves + 1, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 5, spawnShift: 2f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 6,spawnShift: 2.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 2.5f, kindOfDelay: 0));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 8, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneOne());
        }
    }

    private IEnumerator SpawnWavesLvlOneTwo()
    {
        if (PlayerMovement.playerHasControl == true)
        {
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 1));

            StartCoroutine(SpawnThreeSmallEnemies(waveOrder: 2));

            StartCoroutine(SpawnTwoFollowerTypeEnemies(waveOrder: 3));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 4, spawnShift: 1, kindOfDelay: 1));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 5, spawnShift: 0));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 6, spawnShift: 2f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 8, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 9, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 10, spawnShift: 15, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 11, spawnShift: 15, kindOfDelay: 1, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneTwo());
        }
    }

    private IEnumerator SpawnWavesLvlOneThree()
    {
        if (PlayerMovement.playerHasControl == true)
        {
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 1, spawnShift: 5, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 2, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 3, spawnShift: 8, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 4, spawnShift: 7, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 5, spawnShift: 7, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 6, spawnShift: 13, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 11, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 8, spawnShift: 11, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 9, spawnShift: 12, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 10, spawnShift: 11, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 11, spawnShift: 15, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 12, spawnShift: 19, kindOfDelay: 1, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneThree());
        }
    }

    private IEnumerator SpawnWavesLvlOneFour()
    {
        if(PlayerMovement.playerHasControl == true)
        {
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 1, spawnShift: 1, kindOfDelay: 0));

            StartCoroutine(SpawnTwoFollowerTypeEnemies(waveOrder: 2, spawnShift: 6));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 3, spawnShift: 8, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 4, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 5, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 6, spawnShift: 13, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 13, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 8, spawnShift: 11, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 9, spawnShift: 12, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 10, spawnShift: 12, kindOfDelay: 1));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 11, spawnShift: 12, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 12, spawnShift: 14, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 13, spawnShift: 12, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 14, spawnShift: 10, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 15, spawnShift: 15, kindOfDelay: 1, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneFour());
        }
    }

    private IEnumerator SpawnWavesLvlOneFive()
    {
        if (PlayerMovement.playerHasControl == true)
        {
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 2, spawnShift: 3.5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 3));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 4, spawnShift: 1.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 5, spawnShift: 3.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 6, spawnShift: 2.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 1.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 8, spawnShift: 2f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 9));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 10));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 11));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 12, spawnShift: 3.5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 13, spawnShift: 5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 14, spawnShift: 5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 15));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 16, spawnShift: 1.5f, kindOfDelay: 0));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 17, spawnShift: 1.5f, kindOfDelay: 0, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneFive());
        }
    }

    private IEnumerator SpawnWavesLvlOneSix()
    {
        if(PlayerMovement.playerHasControl)
        {
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 0, isFirstWave: true));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 2));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 3, spawnShift: 2, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 4, spawnShift: 2, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 5, spawnShift: 2.5f, kindOfDelay: 0));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 6, spawnShift: 5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 7, spawnShift: 5.5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 8, spawnShift: 10.5f, kindOfDelay: 1));
            
            StartCoroutine(SpawnMultipleEnemies(waveOrder: 9, spawnShift: 16.5f, kindOfDelay: 1));

            StartCoroutine(SpawnMultipleEnemies(waveOrder: 10, spawnShift: 17.5f, kindOfDelay: 1));

            StartCoroutine(SpawnSingleEnemy(waveOrder: 11, spawnShift: 22f, kindOfDelay: 1, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneSix());
        }    
    }

    private IEnumerator SpawnWavesLvlOneSeven()
    {
        if (PlayerMovement.playerHasControl)
        {
            StartCoroutine(SpawnSingleEnemy(waveOrder: 0, isFirstWave: true, isLastWave: true));

            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnWavesLvlOneSeven());
        }
    }
}