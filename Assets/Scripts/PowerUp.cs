using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Health health;
    Shooter shooter;

    void Awake() 
    {
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag =="Power")
        {
            shooter.StartShootingUpgrade();
            Destroy(other.gameObject);
        }
        else if(other.tag =="Health")
        {
            health.IncreaseHealth(10);
            Destroy(other.gameObject);
        }
        else if(other.tag =="Shield")
        {
            health.IncreaseShield(10);
            Destroy(other.gameObject);
        }
    }
}
