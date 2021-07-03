using UnityEngine;

public class EnemyShootingParameters : MonoBehaviour
{
    public enum ProjectileType
    {
        single,
        doubled,
        rotatingBomb,
        targeted,
    }
    public ProjectileType projectileType;

    public enum ProjectileSound
    {
        middlePitch,
        lowPitch,
        highPitch,
        noSound,
        middlePitchVariantTwo
    }
    public ProjectileSound projectileSound;

    [Header("Common projectile parameters")]
    public GameObject enemyLaser;
    public float minTimeBetweenShots = 0.2f;
    public float maxTimeBetweenShots = 3f;
    public float shotCounter;
    public int numberOfRepeats = 2;
    public float timeBetweenBursts = 0.1f;
    public Vector2 vectorTwoProjectileSpeed = new Vector2(0f, -20f);
    public float floatProjectileSpeed;
    public float bombRotationSpeed = 200f;

    [Header("Padding for spawning projectiles")]
    public float verticalPadding = 0.7f;
    public float horizontalPadding = 0.2f;

    [Header("Targeted shooting")]
    public Transform playerPos;
    public Vector2 addForceRandomization;

    [Header("Shooting params for space stations and boss")]
    public Vector2 relativeForce;
    public float rotationSpeed;
    public float movementSpeed;
    public float shotCounterForRedSpaceStation = 0.25f;
    public float shotCounterForGreySpaceStation = 0.2f;
    protected Quaternion zRotation;
}
