using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    // скрипт отвечающий для проверки свайпа которые сделал игрок
    public delegate void OnSwipeInput(SwipeType type);
    public event OnSwipeInput SwipeEvent;

    private bool _isDragging, _isMobilePlatform;
    private Vector2 _tapPoint, _swipeDelta;
    private float _minSwipeDelta = 130;

    public enum SwipeType
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    private void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDLONE
         _isMobilePlatform = false;
        #else
          _isMobilePlatform = true;
        #endif

    }

    private void Update()
    {
        if(!_isMobilePlatform)// игрок сидит с компьютера или телефона
        {
            if(Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _tapPoint = Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if(Input.touchCount > 0) // первоначальные координаты свайпа игрока
            {
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _tapPoint = Input.touches[0].position;
                }
                else if(Input.touches[0].phase == TouchPhase.Canceled || 
                        Input.touches[0].phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        CalculateSwipe();
    }
    private void ResetSwipe()
    {
        _isDragging = false;
        _tapPoint = _swipeDelta = Vector2.zero;
    }

    void CalculateSwipe() // нахождение длины свайпа
    {
        _swipeDelta = Vector2.zero;
        if(_isDragging)
        {
            if (!_isMobilePlatform && Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
            }
            else if(Input.touchCount > 0)
            {
                _swipeDelta = Input.touches[0].position - _tapPoint;
            }
        }
        if(_swipeDelta.magnitude > _minSwipeDelta)
        {

            if(SwipeEvent != null)
            {
                
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                    SwipeEvent(_swipeDelta.x < 0 ? SwipeType.LEFT : SwipeType.RIGHT);
                }
                else
                {
                    SwipeEvent(_swipeDelta.y < 0 ? SwipeType.DOWN : SwipeType.UP);
                }
            }
            ResetSwipe();
        }

    }
}
