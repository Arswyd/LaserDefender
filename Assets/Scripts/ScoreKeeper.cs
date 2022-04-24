using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    static ScoreKeeper instance;
    int currentScore = 0;
    int maxScore = 0;

    void Awake() 
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetCurrentScore()
    {
       return currentScore;
    }

    public int GetMaxScore()
    {
       return maxScore;
    }

    public void SetMaxScore()
    {
        if(maxScore < currentScore)
        {
            maxScore = currentScore;
        }
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
