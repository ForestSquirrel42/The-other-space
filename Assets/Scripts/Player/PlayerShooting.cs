using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public enum LaserType
    {
        single,
        doubled
    }
    public LaserType laserType;
    
    [Header("Weapon and shooting params")]
    [SerializeField] float misslesSpeed = 30f;
    [SerializeField] List<GameObject> lasers;
    [SerializeField] float profectileFiringPeriod = 0.2f;
    private Vector2 laserPlacement;

    private void Start()
    {
        StartCoroutine(FireContiniously());
    }

    IEnumerator FireContiniously()
    {
        if (PlayerMovement.playerHasControl == true)
        {
            while (PlayerMovement.playerHasControl == true)
            {
                laserPlacement = new Vector2(transform.position.x, transform.position.y + 1.7f);

                GameObject laser = Instantiate(lasers[(int)laserType], laserPlacement, Quaternion.identity);

                if(laserType == LaserType.single)
                {
                    laser.GetComponent<PlayerWeaponLogic>().SetPlayerWeaponDamage(DataManager.GetPlayerDamage());
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, misslesSpeed);
                }
                else
                {
                    var playerDamage = laser.GetComponentsInChildren<PlayerWeaponLogic>();
                    var rb = laser.GetComponentsInChildren<Rigidbody2D>();

                    foreach(PlayerWeaponLogic damage in playerDamage)
                    {
                        damage.SetPlayerWeaponDamage(DataManager.GetPlayerDamage());
                    }

                    foreach(Rigidbody2D rigidbody in rb)
                    {
                        rigidbody.velocity = new Vector2(0f, misslesSpeed);
                    }
                }
                
                yield return new WaitForSeconds(profectileFiringPeriod);
                Destroy(laser, 5f);
            }

            if(!PlayerMovement.playerHasControl)
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(FireContiniously());
            }
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FireContiniously());
        }
    }
}
