using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private UnityEngine.UI.Button _shopButton;
    private bool _isOpenShop = false;

    private void Start()
    {
        OpenShopWithBtn();
    }
    private void OpenShopWithBtn()
    {
        _shopButton.onClick.AddListener(ToggleShop);
    }
    private void ToggleShop()
    {
        if(_isOpenShop)
        {
            _shopPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            _shopPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        _isOpenShop = !_isOpenShop;
    }
}
