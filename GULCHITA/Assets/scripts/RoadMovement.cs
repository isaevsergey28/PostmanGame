using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private Vector3 _moveVec;
    private float _speed;

    public List<GameObject> allSwipeObjects = new List<GameObject>();

    private void Start()
    {
        
        _moveVec = new Vector3(0, 0, -1);
    }

    private void Update()
    {
        if(PersonMovement.PlayerMove.GetPlayerLife())// если игрок жив то дорога движется
        {
            _speed = GameController.gameController.addSpeed();
            transform.Translate(_moveVec * Time.deltaTime * _speed);
        }

        foreach(var swipeObject in allSwipeObjects.ToList())
        {
            if (swipeObject.transform.position.x - PersonMovement.PlayerMove.transform.position.x < 50.0f && swipeObject.GetComponent<SwipeObject>().isActive)
            {
                swipeObject.gameObject.SetActive(true);
            }
            else
            {
                swipeObject.gameObject.SetActive(false);
            }
        }
    }
}
