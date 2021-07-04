using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] float minTimeBetweenSpawns, maxTimeBetweenSpawns;

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        if(PlayerMovement.playerHasControl)
        {
            while (PlayerMovement.playerHasControl)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-3.3f, 3.3f), 12f, -2f);

                yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));

                Instantiate(asteroid, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(SpawnAsteroids());
        }
    }
}
