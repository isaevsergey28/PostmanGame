using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonMovement : MonoBehaviour
{
    // скрипт отвечающий за поведение игрока
    static public PersonMovement PlayerMove { get; private set; }

    private bool _isAlive = true;
    private bool _isFired = false;

    private int _laneNumber = 1;
    private int _laneCount = 2;

    private int _inARowNewspaper = 5;

    Animator _animator;// аниматор бега

    public float firstLanePos;// положение первой линии
    public float laneDistance; // длина одной полосы
    public float sideSpeed; // скорость смещения

    public Texture2D lifeIcon;


    private void Awake ()
    {
        PlayerMove = this;
    }
    private void Start()
    {
        _animator = transform.GetChild(1).GetComponent<Animator>();
    }
    public void IncrementLife() // при правильном свайпе увеличение жизни
    {
        _inARowNewspaper++;
        _inARowNewspaper = Mathf.Clamp(_inARowNewspaper, 0, 5);
    }
    public int getLaneNumber()
    {
        return _laneNumber;
    }
    void FixedUpdate()// смещение в разные сстороны
    {
        
        if (_isAlive)
        {
            Vector3 curPos = transform.position;
            curPos.z = Mathf.Lerp(curPos.z, firstLanePos + (_laneNumber * laneDistance), Time.fixedDeltaTime * sideSpeed);
            transform.position = curPos;
        }
    }
    // кнопки для смещения
   public void OnRightButtonDown()
    {
        _laneNumber += 1;
        _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
    }
    public void OnLeftButtonDown()
    {
        _laneNumber -= 1;
        _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
    } 
    public void OnButtonUp()
    {
    }
    public bool GetPlayerLife()
    {
        return _isAlive;
    }
    public bool GetPlayerStatus()
    {
        return _isFired;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Car" && _isAlive)
        {
            PlayerDeath();
        }
        else if (other.collider.tag == "WorkDetecter")
        {
            _inARowNewspaper--;
            _inARowNewspaper = Mathf.Clamp(_inARowNewspaper, 0, 5);
            if (_inARowNewspaper == 0)
            {
                PlayerDeath();
                PlayerDismissal();
            }
        }
    }
    private void PlayerDeath() // смерть игрока
    {
        GetComponent<Collider>().enabled = false;

        _animator.SetBool("isAlive", false);
        CameraTarget tar = this.gameObject.GetComponentInChildren<CameraTarget>();
        tar.DisableTarget();
        _isAlive = false;
    }

    private void PlayerDismissal() // если игрок проиграл по причине потери всех жизней то срабатывает функция увольнения
    {
        GetComponent<Collider>().enabled = false;

        _animator.SetBool("isAlive", false);
        CameraTarget tar = this.gameObject.GetComponentInChildren<CameraTarget>();
        tar.DisableTarget();
        _isFired = true;
    }

    private void OnGUI()
    {
        DisplayLife();
    }
    private void DisplayLife() // вывод жизни на экран
    {
        if(_isAlive)
        {
            Rect newspapRect = new Rect(65, 150, 50, 50);
            GUI.DrawTexture(newspapRect, lifeIcon);
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.black;
            Rect labelRect = new Rect(newspapRect.xMax + 10, newspapRect.y + 10, 60, 60);
            GUI.Label(labelRect, _inARowNewspaper.ToString(), style);
        }
    }
}
