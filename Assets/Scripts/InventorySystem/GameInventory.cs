using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameInventory : Inventory
{
    [SerializeField] private float _characterSpawnDelaySeconds;
    [SerializeField] private List<Text> _currentCoinsText;
    [SerializeField] private Button _stopGameButton;

    private int _currentOpenCells;
    private int _currentCoins;
    private float _currentTime;
    private bool _canSpawn;
    private Target _target;

    public event UnityAction FullInventoryEvent;
    public event UnityAction TargetComplete;

    public int CurrentCoins => _currentCoins;

    private void OnEnable()
    {
        _stopGameButton.onClick.AddListener(StopGame);
    }

    private void OnDisable()
    {
        _stopGameButton.onClick.RemoveListener(StopGame);
    }

    private void Awake()
    {
        _currentCoins = 0;
        UpdateText();

        _currentTime = 0;
        _canSpawn = true;
    }

    private void FixedUpdate()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _characterSpawnDelaySeconds)
        {
            _currentTime = 0;

            if (_canSpawn)
                AddNewCharacter();
        }
    }

    public void UpdateText()
    {
        foreach (var text in _currentCoinsText)
            text.text = $"Добыча: {_currentCoins}";
    }

    private void StopGame()
    {
        FullInventoryEvent?.Invoke();
        _canSpawn = false;
    }

    public void LoadInventory(List<Character> characters, Target target)
    {
        _characters = characters;
        _target = target;
        _currentOpenCells = _target.TargetCharacter.Level + 2;

        if (_currentOpenCells > _characters.Count)
            _currentOpenCells = _characters.Count;

        for (int i = 0; i < _currentOpenCells; i++)
        {
            _cells[i].gameObject.SetActive(true);
            _cells[i].LoadCell(i, this);
        }
    }

    private void AddNewCharacter()
    {
        for (int i = 0; i < _currentOpenCells; i++)
        {
            if (_cells[i].IsFree)
            {
                AudioManager.Instance.SpawnCharacter();

                _cells[i].AddPerson(_characters[0]);
                return;
            }
        }

        FullInventoryEvent?.Invoke();
        _canSpawn = false;
    }

    public override Character Merge(Character character)
    {
        int currentPersId = character.Level - 1;
        int targetPersId = currentPersId + 1;

        if (targetPersId >= _characters.Count)
            targetPersId = _characters.Count - 1;

        if (_characters[targetPersId] == _target.TargetCharacter)
        {
            TargetComplete?.Invoke();
            _canSpawn = false;
        }

        AudioManager.Instance.CharacterClip(_characters[targetPersId].Clip);

        _currentCoins += _characters[targetPersId].Level;
        UpdateText();

        return _characters[targetPersId];
    }

    public override void ChangeInventoryId(int id, int currentCharacterLevel)
    {

    }
}
