using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIGameplayController : MonoBehaviour
{
    //UI Time
    [SerializeField] private TextMeshProUGUI _timeText;
    protected float _elapseTime = 0f;
    protected bool _isPlaying = false;
    protected float _gameSpeed = 1f;
    //UI Wave
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private SpawnManager _spawnManager;

    private void Start()
    {
        InitializeTime();   
    }

    private void InitializeTime()
    {
        if (_timeText == null)
        {
            _timeText = GetComponent<TextMeshProUGUI>();
            if(_timeText == null)
            {
                Debug.Log("timeText is not assigned and TextMeshProUGUI component is not found.");
            }
        }
    }
    private void InitiallizeWave()
    {
        if(_waveText == null)
        {
            _waveText = GetComponent<TextMeshProUGUI>();
            if(_waveText == null)
            {
                Debug.Log("waveText is not assigned and TextMeshProUGUI component is not found.");
            }
        }
        UpdateWaveText();
    }
    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (!_isPlaying)
        {
            _elapseTime += Time.deltaTime * _gameSpeed;
            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        if(_timeText != null)
        {
            int minutes = Mathf.FloorToInt(_elapseTime / 60);
            int seconds = Mathf.FloorToInt(_elapseTime % 60);
            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void UpdateWaveText()
    {
        if( _waveText != null && _spawnManager != null)
        {
            _waveText.text = "Wave: " + _spawnManager.GetCurrentWave().ToString() + "  / 40";
        }
    }
    private void OnEnable()
    {
        InitializeTime();
    }
}
