using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;
using System;

public class UIShop : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _upgradeBtn;
    [SerializeField] private UnityEngine.UI.Image _upgradeImageBtn;
    [SerializeField] private Color _whiteColor = Color.white;
    [SerializeField] private Color _redColor = Color.red;
    [SerializeField] private float _colorResetDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        SetColorOnClicked();
    }
    private void SetColorOnClicked()
    {
        _upgradeBtn.onClick.AddListener(OnUpgradeBtnClicked);
    }

    public void OnUpgradeBtnClicked()
    {
        _upgradeImageBtn.color = _redColor;

        StartCoroutine(RevertToWhiteAfterDelay());
    }
    private IEnumerator RevertToWhiteAfterDelay()
    {
        yield return new WaitForSecondsRealtime(_colorResetDelay);
        ResetToDefaultColor();
    }
    private void ResetToDefaultColor()
    {
        _upgradeImageBtn.color = _whiteColor;
    }
}
