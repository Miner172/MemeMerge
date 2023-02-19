using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour, IDropHandler
{
    [SerializeField] private CellView _view;
    [SerializeField] private CellMover _mover;
    [SerializeField] private RectTransform _itemPlace;
    
    private Inventory _inventory;
    private Character _character;
    private Image _image;
    private bool _isFree;
    private int _id;

    public Character Character => _character;
    public bool IsFree => _isFree;

    private void OnEnable()
    {
        _mover.OnBeginDragCell += OnBeginDragCell;
        _mover.OnEndDragCell += OnEndDragCell;
    }

    private void OnDisable()
    {
        _mover.OnBeginDragCell -= OnBeginDragCell;
        _mover.OnEndDragCell -= OnEndDragCell;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();

        _view.gameObject.SetActive(false);
        _isFree = true;
    }

    public void LoadCell(int id, Inventory inventory)
    {
        _inventory = inventory;
        _id = id;
    }

    public void AddPerson(Character character)
    {
        _character = character;

        _view.gameObject.SetActive(true);
        _view.LoadView(_character);

        _isFree = false;

        Changeover();
    }

    public void RemovePerson()
    {
        _character = null;

        _view.gameObject.SetActive(false);
        _isFree = true;

        Changeover();
    }

    private void Changeover()
    {
        int currentCharacterLevel;

        if (_character == null)
            currentCharacterLevel = 0;
        else
            currentCharacterLevel = _character.Level;

        _inventory.ChangeInventoryId(_id, currentCharacterLevel);
    }

    private void OnBeginDragCell()
    {
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    private void OnEndDragCell()
    {
        _image.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        CellLink cell = eventData.pointerDrag.GetComponent<CellLink>();

        if (_isFree)
            Transition(cell);
        else
        {
            if (cell.Character.Level != _character.Level)
                Replace(cell);
            else
                Merge(cell);
        }
    }

    private void Transition(CellLink cell)
    {
        AudioManager.Instance.Swap();

        AddPerson(cell.Character);
        cell.RemovePerson();
    }

    private void Replace(CellLink cell)
    {
        AudioManager.Instance.Swap();

        Character tempCharacter = _character;

        AddPerson(cell.Character);
        cell.AddPerson(tempCharacter);
    }

    private void Merge(CellLink cell)
    {
        cell.RemovePerson();
        AddPerson(_inventory.Merge(_character));
    }
}
