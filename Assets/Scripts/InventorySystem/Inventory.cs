using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected List<Cell> _cells;

    protected List<Character> _characters;

    private void Awake()
    {
        for (int i = 0; i < _cells.Count; i++)
            _cells[i].LoadCell(i, this);
    }

    public abstract void ChangeInventoryId(int id, int currentCharacterLevel);

    public abstract Character Merge(Character character);
}
