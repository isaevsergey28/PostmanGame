using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ArrowsController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image _sprite;
    private Color _defaultColor;

    private void Start()
    {
        _sprite = GetComponent<Image>();
        _defaultColor = _sprite.color;
        _defaultColor.a = 0.4f;
        _sprite.color = _defaultColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _defaultColor = _sprite.color;
        _defaultColor.a = 0.9f;
        _sprite.color = _defaultColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _defaultColor = _sprite.color;
        _defaultColor.a = 0.2f;
        _sprite.color = _defaultColor;
    }
}
