using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class Energy : MonoBehaviour
{
    [SerializeField] private int _rewardId;
    [SerializeField] private int _energyForReward;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private Text _maxEnergyText;
    [SerializeField] private Text _energyText;
    [SerializeField] private Text _energyForRewardText;
    [SerializeField] private Button _showRewardButton;

    private int _currentEnergy;
    public int CurrentEnergy => _currentEnergy;

    public event UnityAction<string> OnError;

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

    public void LoadCurrentEnergy(int coins)
    {
        _currentEnergy = coins;
        UpdateText();
    }

    private void Reward(int id)
    {
        if (_rewardId == id)
            AddEnergy(_energyForReward);
    }

    public bool AddEnergy(int value)
    {
        int amount = value + _currentEnergy;

        if (amount > _maxEnergy)
        {
            OnError?.Invoke("” вас макс. молний");

            return false;
        }
        else
        {
            AudioManager.Instance.AddLightings();

            _currentEnergy += value;
            UpdateText();

            return true;
        }
    }

    public bool RemoveEnergy(int value)
    {
        if (value > _currentEnergy)
            return false;
        else
        {
            _currentEnergy -= value;
            UpdateText();

            return true;
        }
    }

    private void UpdateText()
    {
        _energyForRewardText.text = $"{_energyForReward} молни€ за рекламу";
        _maxEnergyText.text = $"макс. 5";
        _energyText.text = $":{_currentEnergy}";
    }
}
