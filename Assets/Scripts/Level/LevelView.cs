using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _level;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Button _button;

    private int _levelId;
    private LevelManager _levelManager;

    private void OnEnable()
    {
        _button.onClick.AddListener(StartLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartLevel);
    }

    public void LoadView(int level, bool isOpen, Character charater, LevelManager levelManager)
    {
        _levelManager = levelManager;
        _levelId = level;

        if (isOpen)
        {
            _lock.SetActive(false);
            _button.interactable = true;
            _level.text = $"{_levelId + 1}";
        }
        else
        {
            _lock.SetActive(true);
            _button.interactable = false;
            _level.text = $"???";
        }

        _icon.sprite = charater.Icon;
    }

    private void StartLevel()
    {
        _levelManager.StartLevel(_levelId);
    }
}
