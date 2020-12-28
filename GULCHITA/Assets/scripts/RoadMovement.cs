using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private Vector3 _moveVec;
    private float _speed;

    public List<GameObject> swipeObjects;

    private void Start()
    {
        
        _moveVec = new Vector3(0, 0, -1);
        foreach (var item in swipeObjects)
        {
            float tempX = Random.Range(0, 20);
            Vector3 tempPos = item.transform.position;
            tempPos.x += tempX;
            item.transform.position = tempPos;
            item.SetActive(false);
        }
    }

    private void Update()
    {
        if(PersonMovement.PlayerMove.GetPlayerLife())// если игрок жив то дорога движется
        {
            _speed = GameController.instruction.addSpeed();
            transform.Translate(_moveVec * Time.deltaTime * _speed);
        }
        foreach (var item in swipeObjects)
        {
            if(item.transform.position.x - PersonMovement.PlayerMove.transform.position.x < 30.0f)
            {
                item.SetActive(true);
            }
        }
    }
}
