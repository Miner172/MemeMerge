using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _iconImage;

    private Character _character;

    public void LoadView(Character character)
    {
        _character = character;

        _levelText.text = _character.Level.ToString();

        _iconImage.sprite = _character.Icon;
    }
}
