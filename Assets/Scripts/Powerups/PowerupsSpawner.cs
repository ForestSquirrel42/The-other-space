using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawner : MonoBehaviour
{
    [SerializeField] GameObject powerup;
    [SerializeField] float minTimeBeforeSpawn = 30f;
    [SerializeField] float maxTimeBeforeSpawn = 60f;

    private void Start()
    {
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBeforeSpawn, maxTimeBeforeSpawn));
        while(true)
        {
            if(PlayerMovement.playerHasControl)
            {
                Instantiate(powerup, new Vector2(Random.Range(3f, -3f), 12.85f), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(minTimeBeforeSpawn, maxTimeBeforeSpawn));
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
