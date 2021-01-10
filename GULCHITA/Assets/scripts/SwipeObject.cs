using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeObject : MonoBehaviour
{
    private GameObject _swipeObject; // сохранение объекта свайпа  на который наступил игрок
    // скрипт для объектов свайпа
    public static SwipeObject swipeObject { get; private set; }

    private bool _isTrigger = false;
    private BoxCollider _boxCollider;

    public bool isActive { get; private set; }

    private void Awake()
    {
        swipeObject = this;
    }
    private void Start()
    {
        isActive = DetermineIfActive();
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }
    public bool DetermineIfActive()// рандом для объектов свайпа
    {
        return Random.Range(0, 2) == 1 ? true : false;
    }
   private void OnTriggerEnter(Collider other)
   {
        if(!_isTrigger && other.gameObject.CompareTag("Player"))
        {
            GameController.gameController.showPicture(gameObject);
            _isTrigger = true;
        }
   }
    private void OnTriggerExit(Collider other)
    {
        if (_isTrigger && other.gameObject.CompareTag("Player"))
        {
            GameController.gameController.hidePicture();
            _isTrigger = false;
        }
    }
    public void ColliderOff(GameObject other) // отключение объекта который отнимает у игрока жизнь если игрок сделал правильный свайп
    {
        other.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }

    public void SaveSwipeObject(GameObject other)//когад игрок наступил на коллайдер объекта свайпа мы сохраняем его
    {
        _swipeObject = other;
    }
    public void ChangeSwipeColor(Color newColor)
    {
        _swipeObject.GetComponent<MeshRenderer>().material.color = newColor;
    }
}
