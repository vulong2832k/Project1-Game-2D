using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TankStats _tankStats;
    [SerializeField] private UIShopLevelAttribute _uiShopLevelAttribute;

    [Header("UI Panel")]
    [SerializeField] protected GameObject pauseMenuUI;
    [SerializeField] protected GameObject GameOverMenuUI;
    private bool isPaused = false;
    private bool isPlayerDead = false;
    public SpawnManager spawnManager;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;

    [Header("UI AudioSetting")]
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Button _exitSettingPanel;

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
        if(_audioManager != null)
        {
            _audioManager.PlaySFX(_audioManager.playerDead);
            _audioManager.StopMusic();
        }
        isPlayerDead = true;
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        if (_tankStats != null)
        {
            _tankStats.ResetToDefault();
        }
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGame()
    {
        _tankStats.ResetToDefault();
        Time.timeScale = 1f;
        _uiShopLevelAttribute.UpdateUI(_tankStats);
        spawnManager.ResetEnemyData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenSettingPanel()
    {
        _settingPanel.SetActive(true);
    }
    public void ExitSettingPanel()
    {
        _settingPanel.SetActive(false);
    }
}
