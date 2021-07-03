using System.Collections;
using UnityEngine;
using System;

public class EnemyShootingTypes : EnemyShootingParameters
{
    protected void FireNormalType(Action playLaserSFX)
    {
        var releaseShot = Instantiate(enemyLaser, new Vector3(transform.position.x, transform.position.y - verticalPadding, transform.position.z), Quaternion.Euler(0, 0, 0));
        releaseShot.GetComponent<Rigidbody2D>().velocity = vectorTwoProjectileSpeed;

        playLaserSFX();
    }

    protected IEnumerator ReleaseTwoProjectiles(Action playLaserSFX)
    {
        for (int i = 0; i < numberOfRepeats; i++)
        {
            var releaseFirstShot = Instantiate(enemyLaser, new Vector3(transform.position.x + horizontalPadding, transform.position.y - verticalPadding, transform.position.z), Quaternion.identity);
            releaseFirstShot.GetComponent<Rigidbody2D>().velocity = vectorTwoProjectileSpeed;

            var releaseSecondShot = Instantiate(enemyLaser, new Vector3(transform.position.x - horizontalPadding, transform.position.y - verticalPadding, transform.position.z), Quaternion.identity);
            releaseSecondShot.GetComponent<Rigidbody2D>().velocity = vectorTwoProjectileSpeed;

            playLaserSFX();

            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
   
    protected void ReleaseRotatingBomb(Action playLaserSFX)
    {
        var releaseBomb = Instantiate(enemyLaser, new Vector3(transform.position.x, transform.position.y - verticalPadding, transform.position.z), Quaternion.identity);
        Rigidbody2D rb = releaseBomb.GetComponent<Rigidbody2D>();

        rb.velocity = vectorTwoProjectileSpeed;
        rb.angularVelocity = bombRotationSpeed;

        playLaserSFX();
    }

    protected IEnumerator FireTargeted(Action playLaserSFX)
    {
        Vector3 dir = playerPos.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90; // calculate angle to turn projectile towards player
        var rota = Quaternion.AngleAxis(angle, Vector3.forward); // set projectile rotation relative to player

        for (int i = 0; i < numberOfRepeats; i++)
        {
            var releaseShot = Instantiate(enemyLaser, transform.position, rota);
            var rb = releaseShot.GetComponent<Rigidbody2D>();

            rb.AddRelativeForce(Vector2.down * floatProjectileSpeed);
            rb.AddForce(new Vector2(UnityEngine.Random.Range(-addForceRandomization.x, addForceRandomization.x), 0));

            playLaserSFX();

            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
}