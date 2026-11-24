using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winFirstButton;
    [SerializeField] GameObject loseFirstButton;
    [SerializeField] TMP_Text winTimeText;
    [SerializeField] LevelLoader loader;
    [SerializeField] int targetScore = 5;
    [SerializeField] bool requireExit;
    [SerializeField] int startingLives = 3;
    [SerializeField] bool requireSurvival;
    bool survivalComplete;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip loseClip;

    int score;
    int lives;
    bool gameEnded;
    bool respawning;

    Health playerHealth;
    Rigidbody playerBody;
    Vector3 spawnPosition;

    public bool IsPlaying => !gameEnded && !respawning;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        lives = startingLives;
    }

    void Start()
    {
        playerHealth = FindAnyObjectByType<Health>();
        playerBody = playerHealth.GetComponent<Rigidbody>();
        spawnPosition = playerBody.position;

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !PauseMenu.IsPaused)
            Restart();

        if (IsPlaying && requireSurvival && survivalComplete &&
            score >= targetScore && !requireExit)
        {
            Won();
        }
    }

    public void AddScore(int amount)
    {
        if (!IsPlaying) return;

        score += amount;
        UpdateUI();

        if (score >= targetScore && !requireExit && !requireSurvival)
            Won();
    }

    public void UpdateUI()
    {
        scoreText.text = $"Coins: {score} / {targetScore}";
        livesText.text = $"Lives: {lives}";
    }

    public void PlayerDied()
    {
        if (!IsPlaying) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
            Lost();
        else
            StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        respawning = true;
        yield return new WaitForSeconds(0.6f);

        playerBody.position = spawnPosition;
        playerBody.linearVelocity = Vector3.zero;
        playerBody.angularVelocity = Vector3.zero;
        playerHealth.Restore(1.5f);

        respawning = false;
        UpdateUI();
    }

    void Won()
    {
        int level = gameObject.scene.buildIndex;
        float time = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, time);
        SaveData.Unlock(level + 1);
        ShowWinScreen(best, time);
    }

    void ShowWinScreen(bool best, float time)
    {
        int level = gameObject.scene.buildIndex;

        if (best)
            winTimeText.text = $"Time: {time:0.0} s  NEW BEST!";
        else
            winTimeText.text = $"Time: {time:0.0} s  (best {SaveData.BestTime(level):0.0} s)";

        SfxPlayer.Instance.Play(winClip);
        EndGame(winPanel, winFirstButton);
    }

    void Lost()
    {
        SfxPlayer.Instance.Play(loseClip);
        EndGame(losePanel, loseFirstButton);
    }

    void EndGame(GameObject panel, GameObject firstButton)
    {
        gameEnded = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void TryExit()
    {
        if (IsPlaying && requireExit && score >= targetScore)
            Won();
    }

    public void TimeExpired()
    {
        if (!gameEnded)
            Lost();
    }

    public void SurvivalFinished()
    {
        survivalComplete = true;
    }

    public void Restart() => loader.Reload();
    public void NextLevel() => loader.LoadNext();
    public void MainMenu() => loader.LoadMenu();
}