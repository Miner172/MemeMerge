using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelView> _levelViews;
    [SerializeField] private int _energyPriceForOneLevel;
    [SerializeField] private Button _infiniyModeButton;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;
    public int EnergyPriceForOneLevel => _energyPriceForOneLevel;

    public event UnityAction<int> OnLoadLevel;

    private void OnEnable()
    {
        _infiniyModeButton.onClick.AddListener(InfinityMode);
    }

    private void OnDisable()
    {
        _infiniyModeButton.onClick.RemoveListener(InfinityMode);
    }

    public void InfinityMode()
    {
        _currentLevel = -1;

        OnLoadLevel?.Invoke(_currentLevel);
    }

    public void LoadLevels(List<Character> characters, bool[] openLevels)
    {
        for (int i = 0; i < characters.Count; i++)
            _levelViews[i].LoadView(i, openLevels[i], characters[i], this);
    }

    public void StartLevel(int id)
    {
        _currentLevel = id;
        OnLoadLevel?.Invoke(_currentLevel);
    }
}
