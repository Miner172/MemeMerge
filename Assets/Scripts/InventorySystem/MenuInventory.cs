using System.Collections.Generic;
using UnityEngine.Events;

public class MenuInventory : Inventory
{
    private int[] _characterInventoryId;
    private int _bestCharacterId;
    public int[] CharacterInventoryId => _characterInventoryId;
    public int BestCharacterId => _bestCharacterId;

    public event UnityAction OnChangeBestCharater;
    public event UnityAction OnWin;
    public event UnityAction<string> OnError;

    public void LoadInventory(int[] charactersInventoryId, List<Character> characters, int bestCharacterId)
    {
        _bestCharacterId = bestCharacterId;
        _characterInventoryId = charactersInventoryId;

        _characters = characters;

        for (int i = 0; i < _characterInventoryId.Length; i++)
        {
            if (_characterInventoryId[i] != -1)
                _cells[i].AddPerson(_characters[_characterInventoryId[i]]);
        }
    }

    public bool AddNewCharacter(Character character)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].IsFree)
            {
                _cells[i].AddPerson(character);
                return true;
            }
        }

        OnError.Invoke("Нет свободного места в инвенторе");
        return false;
    }

    public override void ChangeInventoryId(int id, int currentCharacterLevel)
    {
        currentCharacterLevel -= 1;

        _characterInventoryId[id] = currentCharacterLevel;
    }

    public override Character Merge(Character character)
    {
        int currentPersId = character.Level - 1;
        int targetPersId = currentPersId + 1;

        if (targetPersId >= _characters.Count)
        {
            targetPersId = _characters.Count -1;
            OnWin?.Invoke();
        }

        if (_bestCharacterId < targetPersId)
        {
            _bestCharacterId = targetPersId;
            OnChangeBestCharater?.Invoke();
        }

        AudioManager.Instance.CharacterClip(_characters[targetPersId].Clip);

        return _characters[targetPersId];
    }
}
