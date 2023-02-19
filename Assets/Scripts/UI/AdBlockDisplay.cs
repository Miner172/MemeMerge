using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdBlockDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _adBlock;
    [SerializeField] private Button _closeAdBlockButton;
    [SerializeField] private List<Button> _showAdBlockButtons;

    private void OnEnable()
    {
        foreach (var showAdBlockButton in _showAdBlockButtons)
            showAdBlockButton.onClick.AddListener(ShowAdBlock);

        _closeAdBlockButton.onClick.AddListener(HideAdBlock);
    }

    private void OnDisable()
    {
        foreach (var showAdBlockButton in _showAdBlockButtons)
            showAdBlockButton.onClick.RemoveListener(ShowAdBlock);

        _closeAdBlockButton.onClick.RemoveListener(HideAdBlock);
    }

    private void Awake()
    {
        HideAdBlock();
    }

    private void ShowAdBlock()
    {
        _adBlock.SetActive(true);
    }

    private void HideAdBlock()
    {
        _adBlock.SetActive(false);
    }
}
