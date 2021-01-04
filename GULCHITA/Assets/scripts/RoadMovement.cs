using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private Vector3 _moveVec;
    private float _speed;

    public GameObject newspaper;

    private void Start()
    {
        
        _moveVec = new Vector3(0, 0, -1);
    }

    private void Update()
    {
        if(PersonMovement.PlayerMove.GetPlayerLife())// если игрок жив то дорога движется
        {
            _speed = GameController.instruction.addSpeed();
            transform.Translate(_moveVec * Time.deltaTime * _speed);
        }
    }
}
