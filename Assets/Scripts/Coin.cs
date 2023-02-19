using UnityEngine;
using UnityEngine.UI;
using YG;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _rewardId;
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _coinForRewardText;
    [SerializeField] private Button _showRewardButton;
    [SerializeField] private Main _main;

    private int _coinForReward;
    private int _currentCoins;

    public int CurrentCoins => _currentCoins;

    private void OnEnable()
    {
        _showRewardButton.onClick.AddListener(ShowReward);
        YandexGame.RewardVideoEvent += Reward;
    }

    private void OnDisable()
    {
        _showRewardButton.onClick.RemoveListener(ShowReward);
        YandexGame.RewardVideoEvent -= Reward;
    }

    private void ShowReward()
    {
        YandexGame.RewVideoShow(_rewardId);
    }

    public void LoadCurrentCoins(int coins)
    {
        _currentCoins = coins;
        UpdateText();
    }

    private void Reward(int id)
    {
        if (_rewardId == id)
        {
            _coinForReward = _main.BestiCharacter.Price / 2;

            AddCoins(_coinForReward);
        }
    }

    public void AddCoins(int value)
    {
        AudioManager.Instance.AddCoins();

        _currentCoins += value;
        UpdateText();
    }

    public bool RemoveCoins(int value)
    {
        if (value > _currentCoins)
            return false;
        else
        {
            _currentCoins -= value;
            UpdateText();

            return true;
        }
    }

    private void UpdateText()
    {
        _coinForReward = _main.BestiCharacter.Price / 2;

        _coinForRewardText.text = $"{_coinForReward} венскоин за рекламу";
        _coinText.text = $":{_currentCoins}";
    }
}
