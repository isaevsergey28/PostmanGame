using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private bool _isPlayerAlive = true;
    public Transform objToFollow;
    private Vector3 _offset;
    private PersonMovement _move;
    private float _indexPos;

    void Start()
    {
        _offset = objToFollow.position - transform.position;
        _move = GetComponentInParent<PersonMovement>();
    }

    void FixedUpdate() // в зависимости от положения цели смещение камеры
    {
        if (_isPlayerAlive)
        {
            
            switch (_move.getLaneNumber())
            {
                case 0:
                    _indexPos = -2.5f;
                    break;
                case 1:
                    _indexPos = 0;
                    break;
                case 2:
                    _indexPos = 2.5f;
                    break;
            }
            transform.position = new Vector3(objToFollow.position.x - _offset.x, objToFollow.position.y - _offset.y, (objToFollow.position.z - _offset.z + _indexPos));

        }
    }
    public void DisableTarget()// если игрок проиграл то у камеры нет смещения
    {
        _isPlayerAlive = false;
        transform.position = new Vector3(objToFollow.position.x - _offset.x, objToFollow.position.y - _offset.y, (objToFollow.position.z - _offset.z));
    }
}
