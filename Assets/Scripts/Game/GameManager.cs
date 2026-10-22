using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ustawienia poziomu")]
    public int startingLives = 3;
    public float startingTime = 300f;
    public int pointsPerSecond = 10;

    [Header("UI")]
    public Text livesText;
    public Text timeText;
    public Text scoreText;

    [Header("Respawn")]
    public GameObject playerBall;

    private int currentLives;
    private float timeLeft;
    private int currentScore = 0;
    private Vector3 currentRespawnPos;
    private bool isGameActive = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = startingLives;
        timeLeft = startingTime;

        if (playerBall != null)
            currentRespawnPos = playerBall.transform.position;

        UpdateUI();
    }

    void Update()
    {
        if (!isGameActive) return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimeUI();
        }
        else
        {
            GameOver("Czas minął!");
        }
    }

    public void LoseLife()
    {
        if (!isGameActive) return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0) GameOver("Brak żyć!");
        else RespawnPlayer();
    }

    public void AddScore(int amount) { if (isGameActive) { currentScore += amount; UpdateScoreUI(); } }
    public void AddTime(float amount) { if (isGameActive) { timeLeft += amount; UpdateTimeUI(); } }

    public void RespawnPlayer()
    {
        if (playerBall == null) return;
        Rigidbody rb = playerBall.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerBall.transform.position = currentRespawnPos;
    }

    public void SetCheckpoint(Vector3 newPos) { currentRespawnPos = newPos; }

    public void LevelComplete()
    {
        if (!isGameActive) return;
        isGameActive = false;
        int timeBonus = Mathf.CeilToInt(timeLeft) * pointsPerSecond;
        currentScore += timeBonus;
        UpdateScoreUI();
        Debug.Log("POZIOM UKOŃCZONY! Bonus: +" + timeBonus + " pkt. Wynik: " + currentScore);
    }

    private void GameOver(string reason)
    {
        if (!isGameActive) return;
        isGameActive = false;
        Debug.Log("GAME OVER: " + reason);
    }

    private void UpdateUI() { UpdateLivesUI(); UpdateTimeUI(); UpdateScoreUI(); }
    private void UpdateLivesUI() { if (livesText != null) livesText.text = "Życia: " + currentLives; }
    private void UpdateTimeUI() { if (timeText != null) timeText.text = "Czas: " + Mathf.CeilToInt(timeLeft).ToString(); }
    private void UpdateScoreUI() { if (scoreText != null) scoreText.text = "Punkty: " + currentScore; }
}