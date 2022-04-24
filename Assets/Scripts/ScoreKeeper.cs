using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int currentScore = 0;

    public int GetCurrentScore()
    {
       return currentScore;
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
