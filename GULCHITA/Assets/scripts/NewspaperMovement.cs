using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewspaperMovement : MonoBehaviour
{
    //скрипт отвечающий за поведение газеты выпущенной игроком
    private float _speed = 15.0f;
    private bool _isNewspaperActive = false;
    public GameObject obj;
    private GameObject _newspap;
    

    private int _vertSpeed = 30;
    private Vector3 _tempPos;

    private int _count = 0;

    private bool _isHigh = false;
    private bool _isLaneGet = false;

    private float _fallingIndexZ;
    private float _risingIndexZ;
    private float _fallingIndexY;
    private float _risingIndexY;
    
    private PersonMovement move;

    private void Start()
    {
        _risingIndexY = 0.1f;
        _fallingIndexY = -0.06f;
        move = GetComponent<PersonMovement>();
       
    }
    private void getLane()// определение в каком направлении будет выпущена газета
    {
        if (!_isLaneGet)
        {
            switch (move.getLaneNumber())
            {
                
                case 0:
                    _risingIndexZ = 0.16f;
                    _fallingIndexZ = 0.16f;
                    break;
                case 1:
                    _risingIndexZ = 0;
                    _fallingIndexZ = 0;
                    break;
                case 2:
                    _risingIndexZ = -0.16f;
                    _fallingIndexZ = -0.16f;
                    break;
            }
        }
        _isLaneGet = true;
    }
    private void FixedUpdate() // движение газеты
    {
        
        if (_isNewspaperActive)
        {
            getLane();
            _newspap.transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (_count <= _vertSpeed && !_isHigh)
            {
                _tempPos = _newspap.transform.position;
                _tempPos = new Vector3(_tempPos.x, _tempPos.y + _risingIndexY, _tempPos.z + _risingIndexZ);
                _newspap.transform.position = _tempPos;
                _count++;
                if (_count == _vertSpeed)
                {
                    _isHigh = true;
                }
            }
            else if (_count >= 0 && _isHigh)
            {
                _tempPos = _newspap.transform.position;
                _tempPos = new Vector3(_tempPos.x, _tempPos.y + _fallingIndexY, _tempPos.z + _fallingIndexZ);
                _newspap.transform.position = _tempPos;
                _count--;
                if (_count <= 0)
                {
                    _isHigh = false;
                    destroyNewspaper();
                }
            }

        }
    }
    public void destroyNewspaper()
    {

        Destroy(_newspap);
        _isNewspaperActive = false;
        _isLaneGet = false;
    }
    public void createNewspaper()
    {
        _isNewspaperActive = true;
        switch(move.getLaneNumber())
        {
            case 0:
                _newspap = (GameObject)Instantiate(obj, new Vector3(0, 1f, 3.5f), transform.rotation);
                break;
            case 2:
                _newspap = (GameObject)Instantiate(obj, new Vector3(0, 1f, -3.5f), transform.rotation);
                break;
        }
        
    }
}
