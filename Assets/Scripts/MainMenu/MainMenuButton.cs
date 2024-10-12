using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuButton : MonoBehaviour
{
    //Setting Panel
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private UnityEngine.UI.Button _settingBtn;
    [SerializeField] private UnityEngine.UI.Button _exitButton;
    private bool _isShowSettingPanel = false;
    //
    private void Start()
    {
        OpenSettingPanelWithBtn();
        ExitSoundPanel();
    }

    private void OpenSettingPanelWithBtn()
    {
        _settingBtn.onClick.AddListener(ShowSettingPanel);
    }
    private void ShowSettingPanel()
    {
        if (_isShowSettingPanel)
        {
            _settingPanel.SetActive(true);
        }
        else
        {
            _settingPanel.SetActive(false);
        }
        _isShowSettingPanel = !_isShowSettingPanel;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void SettingBtn()
    {

    }
    private void ExitSoundPanel()
    {
        _exitButton.onClick.AddListener(RemoveSettingPanel);
    }

    public void RemoveSettingPanel()
    {
        _settingPanel.SetActive(false);
    }
}
