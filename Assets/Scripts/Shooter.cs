using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    enum ShootingType {basic, triple, circular}

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ShootingType shootingType = ShootingType.basic;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 1f;
    [SerializeField] float powerUpgradeTime = 10f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float minFiringRate = 0.1f;
    [SerializeField] float firingRateVariance = 0.5f;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Coroutine powerUpgrade;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        if(useAI)
        {
            yield return new WaitForSeconds(1);
        }

        while(true)
        {
            switch(shootingType)
            {
                case ShootingType.basic:
                ShootProjectile(0f);
                break;

                case ShootingType.triple:
                ShootProjectile(15f);
                ShootProjectile(0f);
                ShootProjectile(-15f);
                break;

                case ShootingType.circular:
                ShootProjectile(180f);
                ShootProjectile(135f);
                ShootProjectile(90f);
                ShootProjectile(45f);
                ShootProjectile(0f);
                ShootProjectile(-45f);
                ShootProjectile(-90f);
                ShootProjectile(-135f);
                break;
            } 

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    private void ShootProjectile(float rotation)
    {
        GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, rotation));

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = (Quaternion.Euler(0f, 0f, rotation) * transform.up) * projectileSpeed;
        }

        Destroy(instance, projectileLifeTime);
    }

    public void StartShootingUpgrade()
    {
        if (powerUpgrade != null)
        {
            StopCoroutine(powerUpgrade);
        }
        powerUpgrade = StartCoroutine(SetShootingUpgrade());
    }

    IEnumerator SetShootingUpgrade()
    {
        shootingType = ShootingType.triple;
        yield return new WaitForSeconds(powerUpgradeTime);
        shootingType = ShootingType.basic;
    }
}
