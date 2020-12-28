using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    // скрипт спавна дорог
    public GameObject[] roadBlocks;

    public Transform player;

    private List<GameObject> _currentRoad = new List<GameObject>();

    private float _startRoadXPos = 0;
    private float _roadCount = 7;
    private float _roadLenght = 0;

    private float _removeRoad;

    private void Start()
    {
        _startRoadXPos = player.position.x + 55f;

        _roadLenght = 120;

      
         
        for (int i = 0; i < _roadCount; i++)
        {
            SpawnRoad();
        }
    }
    
    private void LateUpdate()
    {
        CkeckForSpawn();
    }

    private void CkeckForSpawn()
    {
        _removeRoad = player.position.x - 200f;
        foreach(var road in _currentRoad)
        {
            if(_removeRoad > road.transform.position.x)
            {
                SpawnRoad();
                DestroyRoad();
            }
        }
    }
    private void SpawnRoad()
    {
        GameObject road = (GameObject)Instantiate(roadBlocks[MainMenu.roadNumber], transform); // Random.Range(0, RoadBlocks.Length)
        Vector3 roadXPos;
        if(_currentRoad.Count > 0)
        {
            roadXPos = _currentRoad[_currentRoad.Count - 1].transform.position + new Vector3(_roadLenght, 0, 0);
        }
        else
        {
            roadXPos = new Vector3(_startRoadXPos, 0, 0);
        }
        road.transform.position = roadXPos;
        _currentRoad.Add(road);
    }

    private void DestroyRoad()
    {
        Destroy(_currentRoad[0]);
        _currentRoad.RemoveAt(0);

    }
}
