using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI maxScore;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() 
    {
        currentScore.text = "Current score: \n" + scoreKeeper.GetCurrentScore();
        maxScore.text = "Best score: \n" + scoreKeeper.GetMaxScore();
    }

}
