using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeObject : MonoBehaviour
{
    // скрипт для объектов свайпа
    public static SwipeObject swipeObject { get; private set; }

    private bool _isTrigger = false;
    private BoxCollider _boxCollider;

    private bool isActive;

    private void Awake()
    {
        swipeObject = this;
    }
    private void Start()
    {
        isActive = DetermineIfActive();
    }
    private void LateUpdate()
    {
        if(isActive)// если объект свайпа активный(Random.Range(0, 2) == 1) то идет проверка сравнения расстояния 
        {
            if (transform.position.x - PersonMovement.PlayerMove.transform.position.x < 20.0f)
            {
                gameObject.SetActive(true);
            }
        }
        else// иначе объект становится неактивным
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
            GameController.instruction.showPicture(gameObject);
            _isTrigger = true;
        }
   }
    private void OnTriggerExit(Collider other)
    {
        if (_isTrigger && other.gameObject.CompareTag("Player"))
        {
            GameController.instruction.hidePicture();
            _isTrigger = false;
        }
    }
    public void ColliderOff(GameObject other) // отключение объекта который отнимает у игрока жизнь если игрок сделал правильный свайп
    {
        other.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
}
