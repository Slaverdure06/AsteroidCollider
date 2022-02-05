using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float value;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        KeepAsteroidOnScreen();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider) 
        {
            ScoreSystem score = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
            score.AddScoreAsteroid(value);
            gameObject.SetActive(false);
        }
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) { return; }
        playerHealth.Crash();
    }

    private void KeepAsteroidOnScreen()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOutOfBounds = viewportPos.x > 1 || viewportPos.x < 0 || viewportPos.y > 1 || viewportPos.y < 0;
        if (isOutOfBounds) { gameObject.SetActive(false); }
    }
}
