using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    // скрипт спавна дорог
    public GameObject[] roadBlocks;

    public Transform player;

    private List<GameObject> _currentRoads = new List<GameObject>();

    private float _startRoadXPos = 0;
    private float _roadCount = 7;
    private float _roadLenght = 0;

    private float _removeRoad;

    private void Start()
    {
        _startRoadXPos = player.position.x + 24f; // 55

        _roadLenght = 50; // 120

      
         
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
        _removeRoad = player.position.x - 40f; // 200
        foreach(GameObject road in _currentRoads)
        {
            _currentRoads = _currentRoads.Where(i => i != null).ToList();//проверка не изменился ли наш список для избежания ошибок изменения коллекции
            if(_removeRoad > road.transform.position.x)
            {
                SpawnRoad();
                DestroyRoad();
            }
        }
    }
    private void SpawnRoad()
    {
        GameObject road = (GameObject)Instantiate(roadBlocks[0], transform); // Random.Range(0, RoadBlocks.Length)
        Vector3 roadXPos;
        if(_currentRoads.Count > 0)
        {
            roadXPos = _currentRoads[_currentRoads.Count - 1].transform.position + new Vector3(_roadLenght, 0, 0);
        }
        else
        {
            roadXPos = new Vector3(_startRoadXPos, 0.7f, 0); //y = 0 
        }
        road.transform.position = roadXPos;
        _currentRoads.Add(road);
    }

    private void DestroyRoad()
    {
        Destroy(_currentRoads[0]);
        _currentRoads.RemoveAt(0);

    }
}
