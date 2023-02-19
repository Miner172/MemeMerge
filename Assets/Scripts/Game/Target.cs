using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _level;

    private Character _targetCharacter;

    public Character TargetCharacter => _targetCharacter;

    public void LoadTarget(Character character)
    {
        _targetCharacter = character;

        UpdateView();
    }

    private void UpdateView()
    {
        _icon.sprite = _targetCharacter.Icon;

        _level.text = _targetCharacter.Level.ToString();
    }
}
