using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMovement : MonoBehaviour
{
    // скрипт отвечающий за поведение игрока
    static public PersonMovement PlayerMove { get; private set; }

    private bool _isAlive = true;
    private bool _isFired = false;

    private int _laneNumber = 1;
    private int _laneCount = 2;

    public Text lifeCount;// счет жизни игрока
    private int _inARowNewspaper = 5;

    Animator _animator;// аниматор состояний игрока

    public float firstLanePos;// положение первой линии
    public float laneDistance; // длина одной полосы
    public float sideSpeed; // скорость смещения
    


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
        lifeCount.text = _inARowNewspaper.ToString();
    }
    // кнопки для смещения
   public void OnRightButtonDown()
    {
        if(!GameController.gameController.paused)
        {
            _laneNumber += 1;
            _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
        }
       
    }
    public void OnLeftButtonDown()
    {
        if (!GameController.gameController.paused)
        {
            _laneNumber -= 1;
            _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
        }
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
            Debug.Log("tyt");
            SwipeObject.swipeObject.SaveSwipeObject(other.gameObject.transform.parent.gameObject);
            SwipeObject.swipeObject.ChangeSwipeColor(new Color(1, 0, 0, 0.5f));
            _inARowNewspaper--;
            _inARowNewspaper = Mathf.Clamp(_inARowNewspaper, 0, 5);
            if (_inARowNewspaper == 0)
            {
                PlayerDeath();
                PlayerDismissal();
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Newspaper")
        {
            IncrementLife();
            Destroy(other.gameObject);
        }
    }
    private void PlayerDeath() // смерть игрока
    {
        GetComponent<Collider>().enabled = false;

        _animator.SetTrigger("isDead");
        CameraTarget tar = this.gameObject.GetComponentInChildren<CameraTarget>();
        tar.DisableTarget();
        _isAlive = false;
    }

    private void PlayerDismissal() // если игрок проиграл по причине потери всех жизней то срабатывает функция увольнения
    {
        GetComponent<Collider>().enabled = false;

        _animator.SetTrigger("isFired");
        CameraTarget tar = this.gameObject.GetComponentInChildren<CameraTarget>();
        tar.DisableTarget();
        _isFired = true;
    }

}
