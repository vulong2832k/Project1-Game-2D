using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] protected GameObject pauseMenuUI;
    [SerializeField] protected GameObject GameOverMenuUI;

    private bool isPaused = false;
    private bool isPlayerDead = false;

    public SpawnManager spawnManager;

    void Update()
    {
        if (!isPlayerDead)
        {
            GameMenuControl();
        }
    }
    private void GameMenuControl()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PlayerDied()
    {
        isPlayerDead = true;
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        spawnManager.ResetEnemyData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
