using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class CellMover : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Image _image;
    private Vector2 _startPosition;

    public event UnityAction OnBeginDragCell;
    public event UnityAction OnEndDragCell;

    private void OnEnable()
    {
        ResetPosition();
    }

    private void OnDisable()
    {
        ResetPosition();
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _startPosition = _rectTransform.anchoredPosition;
    }

    private void ResetPosition()
    {
        _rectTransform.anchoredPosition = _startPosition;
        _image.raycastTarget = true;

        OnEndDragCell?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;

        OnBeginDragCell?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta; // / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _startPosition;

        _image.raycastTarget = true;

        OnEndDragCell?.Invoke();
    }
}
