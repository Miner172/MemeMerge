using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _characterDisplay;
    [SerializeField] private Button _characterButton;

    private bool _isCharacterDisplayOpen;

    private void OnEnable()
    {
        _characterButton.onClick.AddListener(ShowOrHideCharacterDisplay);
    }

    private void OnDisable()
    {
        _characterButton.onClick.RemoveListener(ShowOrHideCharacterDisplay);
    }

    private void Awake()
    {
        _isCharacterDisplayOpen = false;
        ShowOrHideCharacterDisplay();
    }

    private void ShowOrHideCharacterDisplay()
    {
        if (!_isCharacterDisplayOpen)
        {
            _characterDisplay.SetActive(true);
            _isCharacterDisplayOpen = true;
        }
        else
        {
            _characterDisplay.SetActive(false);
            _isCharacterDisplayOpen = false;
        }
    }
}
