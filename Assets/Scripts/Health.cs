using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int shield = 50;
    [SerializeField] GameObject shieldObject;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] GameObject[] powerUps;
    [SerializeField] float powerUpPercentage = 0.1f;
    [SerializeField] float powerUpSpeed = 1f;
    
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    EnemySpawner enemySpawner;
    int currenHealth;
    int currentShield;
    bool isReloadingPowerup;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        currenHealth = health;
        if(isPlayer)
        {
            currentShield = 0;
        }
        else
        {
            currentShield = shield;
            if(currentShield > 0)
            {
                shieldObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return currenHealth;
    }

    public void IncreaseHealth(int value)
    {
        currenHealth = Mathf.Clamp(currenHealth + value, 0, health);
    }

    public int GetMaxShield()
    {
        return shield;
    }

    public int GetShield()
    {
        return currentShield;
    }

    public void IncreaseShield(int value)
    {
        currentShield = Mathf.Clamp(currentShield + value, 0, shield);
        shieldObject.SetActive(true);
    }

    void TakeDamage(int damage)
    {
        if(currentShield > 0)
        {
            currentShield -= damage;
            if(currentShield <= 0)
            {
                currentShield = 0;
                shieldObject.SetActive(false);
            }
        }
        else
        {
            currenHealth -= damage;
        }

        audioPlayer.PlayDamagingClip();

        if(currenHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.IncreaseScore(score);
            if(UnityEngine.Random.value <= powerUpPercentage)
            {
                if(!isReloadingPowerup)
                {
                    InstantiatePowerUp();
                    isReloadingPowerup = true;
                }
                else
                {
                    isReloadingPowerup = false;
                }
            }
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void InstantiatePowerUp()
    { 
        int powerUpNum = UnityEngine.Random.Range(0, powerUps.Length);
        GameObject instance = Instantiate(powerUps[powerUpNum], transform.position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector3(0, -1, 0) * powerUpSpeed;
        }
        Destroy(instance.gameObject, 6f);
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void OnDestroy() 
    {
        if(!isPlayer)
        {
            enemySpawner.CheckWinningCondition();
        }
    }
}
