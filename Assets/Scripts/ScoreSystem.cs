using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text multiplierText;
    private float scoreMultiplier = 1f;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    private bool shouldCount = true;
    private float score;
    void Update()
    {
        if (!shouldCount) { return; }

        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
        if (score % 10f <= 0.014f && (int)score != 0) { 
            asteroidSpawner.IncreaseDifficulty();
            scoreMultiplier += 0.01f * (score/10f);
        }
        multiplierText.text = "x"+scoreMultiplier.ToString("0.00");
    }

    public int EndTimer() 
    {
        shouldCount = false;
        scoreText.text = string.Empty;
        multiplierText.text = string.Empty;
        scoreMultiplier = 1f;
        return Mathf.FloorToInt(score);
    }

    public void StartTimer()
    {
        shouldCount = true;
    }
    public void AddScoreAsteroid(float value) 
    {
        score += value;
    }
}
