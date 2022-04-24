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
    }

    void Update() 
    {
        healthSlider.value = playerHealth.GetHealth();
        score.text = scoreKeeper.GetCurrentScore().ToString("000000000");
    }
}
