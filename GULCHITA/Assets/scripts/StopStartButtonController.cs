using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopStartButtonController : MonoBehaviour
{
    public Sprite stopIcon;
    public Sprite startIcon;
    private bool _isGameRunning = true;

    public void ChangeState()
    {
        if(_isGameRunning)
        {
            GetComponent<Image>().sprite = startIcon;
            _isGameRunning = false;
        }
        else
        {
            GetComponent<Image>().sprite = stopIcon;
            _isGameRunning = true;
        }
    }
}
