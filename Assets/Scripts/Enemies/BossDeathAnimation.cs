using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathAnimation : MonoBehaviour
{
    private static BossDeathAnimation instance;

    [SerializeField] GameObject smallerExplosion;
    [SerializeField] List<Transform> explosionPlacements;

    private void Awake()
    {
        instance = this;
    }

    public static IEnumerator PlayBossExplosion()
    {
        CameraShake.Shake(2f, 0.03f);
        instance.GetComponent<EnemyMovement>().StopMoving();
        instance.GetComponent<PlayerFollower>().StopFollowing();

        var guns = instance.GetComponentsInChildren<GunsOfEnemyBosses>();

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].StopShooting();
        }
        for(int i = 0; i < instance.explosionPlacements.Count; i++)
        {
            Instantiate(instance.smallerExplosion, instance.explosionPlacements[i].position, Quaternion.identity);
            yield return new WaitForSeconds(0.75f);
        }
        
        yield return new WaitForSeconds(0.6f);
    }
}
