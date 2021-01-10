using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject[] availableCars;// все доступные машины которые добавляют из инсппкктора

    public List<GameObject> currentCars; // активные машины

    public GameObject player;

    private PersonMovement _playerMove;
    

    private void Start()
    {
        _playerMove = player.GetComponent<PersonMovement>();
    }


    private void LateUpdate()
    {
        if(!GameController.gameController.paused)
        {
            GenerateCarIfRequired();
            if (currentCars.Count > 0)
            {
                foreach (var car in currentCars)
                {
                    car.transform.Translate(new Vector3(0, -1, 0));
                }
            }
        }
        
    }
    

    void GenerateCarIfRequired() // если машину можно вывести значит она создается
    {
        List<GameObject> carsToRemove = new List<GameObject>();
        bool addCar = true;

        float playerX = player.transform.position.x;

        float removeCarX = playerX - 30f;
        float addCarX = playerX + 100f;

        if(currentCars.Count > 0)
        {
            foreach (var car in currentCars)
            {
                if (addCarX < car.transform.position.x)
                {
                    addCar = false;
                }
                if (removeCarX > car.transform.position.x)
                {
                    carsToRemove.Add(car);
                }
            }
        }
        if (addCar)
        {
            AddCar();
        }
        foreach (var car in carsToRemove)
        {
            currentCars.Remove(car);
            Destroy(car);
        }
        
    }
    void AddCar() // создание машины
    {
        if (GetIntervalBetweenCars() == 5)
        {
            int randomIndex = Random.Range(0, availableCars.Length);
            GameObject car = (GameObject)Instantiate(availableCars[randomIndex]);
            car.transform.position = new Vector3(player.transform.position.x + 200f, 1.32f, 0);
            if (currentCars.Count > 1)
            {
                if (Mathf.Abs(currentCars[currentCars.Count - 1].transform.position.x - car.transform.position.x) < 20f)
                {
                 
                    car.transform.position = new Vector3(car.transform.position.x + 50f, car.transform.position.y, 0);
                    Debug.Log(car.transform.position.x);
                }
            }
            currentCars.Add(car);
        }
       
    }
    
    private int GetIntervalBetweenCars() // интервал для рандомной генерации машины
    {
        return Random.Range(0, 100);
    }
}



