using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TradeView : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _priceText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _clipButton;
    [SerializeField] private GameObject _lockView;

    private Character _product;
    private Shop _shop;
    private int _id;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(SellButtonClick);
        _clipButton.onClick.AddListener(CharacterClipButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(SellButtonClick);
        _clipButton.onClick.RemoveListener(CharacterClipButtonClick);
    }

    public void LoadView(Character product, bool isOpen, int id, Shop shop)
    {
        _id = id;
        _product = product;
        _shop = shop;

        _iconImage.sprite = _product.Icon;

        if (isOpen)
        {
            _lockView.SetActive(false);

            _nameText.text = $"{_product.Name}";
            _priceText.text = $"{_product.Price}";
            _levelText.text = $"{_product.Level}";
        }
        else
        {
            _lockView.SetActive(true);

            _nameText.text = $"???";
            _priceText.text = $"???";
            _levelText.text = $"?";
        }
    }   

    private void SellButtonClick()
    {
        AudioManager.Instance.ButtonClick();

        if (_product == null)
            return;

        _shop.TrySell(_id);
    }

    private void CharacterClipButtonClick()
    {
        AudioManager.Instance.CharacterClip(_product.Clip);
    }
}
