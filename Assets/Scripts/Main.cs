using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Main : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Energy _energy;
    [SerializeField] private Shop _shop;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private MenuInventory _inventory;
    [SerializeField] private MenuDisplayController _uiController;
    [SerializeField] private LeaderBoardStats _leaderBoard;
    [SerializeField] private Console _console;
    [SerializeField] private List<Character> _characters;

    [SerializeField] private Button _restartGameProgressButton;

    public Character BestiCharacter => _characters[YandexGame.savesData.BestCharacterId];

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        _energy.OnError += MessageToConsole;
        _inventory.OnChangeBestCharater += ChangeBestCharater;
        _inventory.OnWin += Victory;
        _inventory.OnError += MessageToConsole;
        _levelManager.OnLoadLevel += StartLevel;
        _shop.OnTrySell += TrySellCharater;

        _restartGameProgressButton.onClick.AddListener(RestartGameProgress);
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        _energy.OnError -= MessageToConsole;
        _inventory.OnChangeBestCharater -= ChangeBestCharater;
        _inventory.OnWin -= Victory;
        _inventory.OnError -= MessageToConsole;
        _levelManager.OnLoadLevel -= StartLevel;
        _shop.OnTrySell -= TrySellCharater;

        _restartGameProgressButton.onClick.RemoveListener(RestartGameProgress);
    }

    private void Start()
    {
        //YandexGame.ResetSaveProgress();
        //YandexGame.SaveProgress();

        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void RestartGameProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartLevel(int id)
    {
        if (_energy.RemoveEnergy(_levelManager.EnergyPriceForOneLevel))
        {
            Save();

            AudioManager.Instance.StartLevel();
            SceneManager.LoadScene(1);
        }
        else
            MessageToConsole("Не хватает молний");
    }

    public void MessageToConsole(string message)
    {
        _console.MessageToConsole(message);
    }

    public void Victory()
    {
        _uiController.ShowVictory();
        Save();
    }

    public void TrySellCharater(int id)
    {
        Character sellCharacter = _characters[id];

        if (sellCharacter.Price <= _coin.CurrentCoins)
        {
            if (_inventory.AddNewCharacter(_characters[id]))
            {
                AudioManager.Instance.Sell();
                _coin.RemoveCoins(sellCharacter.Price);
            }
        }
        else
            MessageToConsole("Не хватает венскоинов");
    }

    public void ChangeBestCharater()
    {
        int newBestId = _inventory.BestCharacterId;

        _leaderBoard.UpdateLeaderBoard(_characters[newBestId]);
    }

    public void Save()
    {
        YandexGame.savesData.CurrentLevel = _levelManager.CurrentLevel;
        YandexGame.savesData.CurrentCoins = _coin.CurrentCoins;
        YandexGame.savesData.CurrentEnergy = _energy.CurrentEnergy;
        YandexGame.savesData.CharactersInventoryId = _inventory.CharacterInventoryId;
        YandexGame.savesData.BestCharacterId = _inventory.BestCharacterId;
        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        _coin.LoadCurrentCoins(YandexGame.savesData.CurrentCoins);
        _energy.LoadCurrentEnergy(YandexGame.savesData.CurrentEnergy);

        _shop.LoadShop(_characters, YandexGame.savesData.OpenedLevels);
        _levelManager.LoadLevels(_characters, YandexGame.savesData.OpenedLevels);

        _inventory.LoadInventory(YandexGame.savesData.CharactersInventoryId, _characters, YandexGame.savesData.BestCharacterId);
        _leaderBoard.UpdateLeaderBoard(_characters[YandexGame.savesData.BestCharacterId]);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
