using UnityEngine;

public class CellLink : MonoBehaviour
{
    [SerializeField] private Cell _cell;

    public Character Character => _cell.Character;
    public void RemovePerson()
    {
        _cell.RemovePerson();
    }

    public void AddPerson(Character character)
    {
        _cell.AddPerson(character);
    }
}
