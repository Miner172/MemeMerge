using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Character> _characters;
    [SerializeField] private Target _target;
    [SerializeField] private GameInventory _inventory;
    [SerializeField] private LevelDisplayController _displayController;

    [SerializeField] private Text _levelNameText;
    [SerializeField] private List<Button> _backToMainButtons;
    [SerializeField] private List<Button> _completeLevelButtons;

    private int _currentLevel;
    private int _nextLevel;
    private bool _isInfinity;
    private bool _isCoinSave;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.RewardVideoEvent += SaveCoins;
        _inventory.FullInventoryEvent += Lose;
        _inventory.TargetComplete += Victory;

        foreach (var completeLevelButton in _completeLevelButtons)
            completeLevelButton.onClick.AddListener(CompleteLevel);

        foreach (var button in _backToMainButtons)
            button.onClick.AddListener(BackToMain);
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.RewardVideoEvent -= SaveCoins;
        _inventory.FullInventoryEvent -= Lose;
        _inventory.TargetComplete -= Victory;

        foreach (var completeLevelButton in _completeLevelButtons)
            completeLevelButton.onClick.RemoveListener(CompleteLevel);

        foreach (var button in _backToMainButtons)
            button.onClick.RemoveListener(BackToMain);
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void CompleteLevel()
    {
        _isCoinSave = true;

        AudioManager.Instance.CompleteLevel();

        _nextLevel = _currentLevel + 1;

        if (_nextLevel >= YandexGame.savesData.OpenedLevels.Length - 1)
            _nextLevel = YandexGame.savesData.OpenedLevels.Length - 1;

        Save();
        BackToMain();
    }

    private void SaveCoins(int id)
    {
        _isCoinSave = true;

        YandexGame.savesData.CurrentCoins += _inventory.CurrentCoins;
        YandexGame.SaveProgress();

        BackToMain();
    }

    private void Lose()
    {
        AudioManager.Instance.Lose();

        if (_isInfinity)
            _displayController.Infinity();
        else
            _displayController.Lose();
    }

    private void Victory()
    {
        AudioManager.Instance.Victory();

        if (_isInfinity)
            _displayController.Infinity();
        else
            _displayController.Victory();
    }
    private void BackToMain()
    {
        if (_isCoinSave)
            AudioManager.Instance.AddCoins();
        else
            AudioManager.Instance.LossCoins();

        AudioManager.Instance.GoHome();

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void UpdateText()
    {
        if (_isInfinity)
            _levelNameText.text = $"уровень: INFINITY";
        else
            _levelNameText.text = $"уровень: {_currentLevel + 1}";

    }

    public void Save()
    {
        if (_isInfinity)
            YandexGame.savesData.CurrentCoins += _inventory.CurrentCoins;
        else
        {
            YandexGame.savesData.CurrentCoins += _inventory.CurrentCoins;
            YandexGame.savesData.OpenedLevels[_nextLevel] = true;
        }

        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        _isCoinSave = false;
        _currentLevel = YandexGame.savesData.CurrentLevel;

        if (_currentLevel == -1)
        {
            _isInfinity = true;

            _target.LoadTarget(_characters[_characters.Count - 1]);
            _target.gameObject.SetActive(false);
        }
        else
        {
            _isInfinity = false;

            int tagetPerId = _currentLevel + 1;

            if (tagetPerId >= _characters.Count)
                tagetPerId = _characters.Count - 1;

            _target.LoadTarget(_characters[tagetPerId]);
        }

        _inventory.LoadInventory(_characters, _target);

        UpdateText();
    }
}
