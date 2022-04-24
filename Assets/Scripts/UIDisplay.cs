using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Shield")]
    [SerializeField] Slider shieldSlider;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI score;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() 
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        shieldSlider.maxValue = playerHealth.GetMaxShield();
    }

    void Update() 
    {
        healthSlider.value = playerHealth.GetHealth();
        shieldSlider.value = playerHealth.GetShield();
        score.text = scoreKeeper.GetCurrentScore().ToString("000000000");
    }
}
