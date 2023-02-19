using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplayController : MonoBehaviour
{
    [Header("Displays")]
    [SerializeField] private Display _balance;
    [SerializeField] private Display _back;
    [SerializeField] private Display _menu;
    [SerializeField] private Display _stats;
    [SerializeField] private Display _shop;
    [SerializeField] private Display _play;
    [SerializeField] private Display _level;
    [SerializeField] private Display _settings;
    [SerializeField] private Display _victory;

    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _statsButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _closeVictoryButton;

    private List<Display> _displays;
    private Display _currentShowDisplay;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OpenMenu);
        _statsButton.onClick.AddListener(OpenStats);
        _shopButton.onClick.AddListener(OpenShop);
        _playButton.onClick.AddListener(OpenPlay);
        _levelButton.onClick.AddListener(OpenLevels);
        _closeVictoryButton.onClick.AddListener(HideVictory);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OpenMenu);
        _statsButton.onClick.RemoveListener(OpenStats);
        _shopButton.onClick.RemoveListener(OpenShop);
        _playButton.onClick.RemoveListener(OpenPlay);
        _levelButton.onClick.RemoveListener(OpenLevels);
        _closeVictoryButton.onClick.RemoveListener(HideVictory);

    }

    private void Start()
    {
        _displays = new List<Display>()
        {
            _balance, _back, _menu, _settings,
            _stats, _shop, _play, _level, _victory
        };

        ShowStartInterface();
    }

    public void ShowVictory()
    {
        _victory.Show();
    }

    public void HideVictory()
    {
        _victory.Hide();
    }

    public void ShowStartInterface()
    {
        _currentShowDisplay = _menu;
        _currentShowDisplay.Show();

        _balance.Show();
    }

    public void OpenMenu()
    {
        _currentShowDisplay.Hide();
        _currentShowDisplay = _menu;
        _currentShowDisplay.Show();

        _back.Hide();
    }

    public void OpenStats()
    {
        _currentShowDisplay.Hide();
        _currentShowDisplay = _stats;
        _currentShowDisplay.Show();

        _back.Show();
    }

    public void OpenShop()
    {
        _currentShowDisplay.Hide();
        _currentShowDisplay = _shop;
        _currentShowDisplay.Show();

        _back.Show();
    }

    public void OpenPlay()
    {
        _currentShowDisplay.Hide();
        _currentShowDisplay = _play;
        _currentShowDisplay.Show();

        _back.Show();
    }

    public void OpenLevels()
    {
        _currentShowDisplay.Hide();
        _currentShowDisplay = _level;
        _currentShowDisplay.Show();

        _back.Show();
    }

    private void HideAllDisplays()
    {
        foreach (var display in _displays)
            display.Hide();
    }
}
