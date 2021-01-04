using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperSpawner : MonoBehaviour
{
    private bool isActive;

    private void Start()
    {
        isActive = DetermineIfActive();
    }
    private void LateUpdate()
    {
        if (isActive)
        {
            if (transform.position.x - PersonMovement.PlayerMove.transform.position.x < 30.0f)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public bool DetermineIfActive()
    {
        return Random.Range(0, 2) == 1 ? true : false;
    }
}
