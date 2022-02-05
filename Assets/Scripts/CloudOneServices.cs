using CloudOnce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudOneServices : MonoBehaviour
{
    public static CloudOneServices instance;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitScoreToLeaderboard(int score) 
    {
        Leaderboards.HighScore.SubmitScore(score);
    }
}
