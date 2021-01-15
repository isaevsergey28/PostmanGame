using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    

    public string reasonForEnd { get; private set; }//переменная для вывода в LoserMenu причину проигрыша

    static public GameController gameController { get; private set; }// статическая переменная для доступа из других скриптов
    static public float gameSpeed { get; private set; } = 10f;// начальная скорость игры

    public GameObject _swipeCollider { get; private set; } // хранит переменную spiweObject

    private Vector3 _tempPosPicture; // вектор для хранения позиции стрелочки для свайпа

   

    public List<Sprite> pictures;// стрелочки для свайпа

    private PersonMovement _move; 

    public Texture2D newspaperIcon; // иконка загеты

    public GameObject player;
    private NewspaperMovement2 _newspap;

    private Image _img; // хранит текущую картинку

    public GameObject swipeObj;
    private SwipeController _sc;// переменная отвечающая за подписку на event

    private int _pictureIndex;// индекс картинки

    private bool _isPictureActive = false;
    private bool _isPirtureOnScreen = false;


    public Text runningScore;// очки получаемые за пройденную дистанцию
    public float points { get; private set; }
    public float addingSpeed;// увеличение получения очков за дистанцию

    public Text newspaperScore;// счет заброшенных газет
    public int score { get; private set; }


    public bool paused { get; private set; } = false; // пауза
    public GameObject camera;
    private AudioSource _music;// переменные для остановки музыки


    [HideInInspector] public enum SwipeType// перечисление для 4ех свайпов
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    
    public List<SwipeType> thisType;
    
    private void Awake()// инициализация статик переменной
    {
        gameController = this;
    }
    
    private void Start()
    {
        gameSpeed = 10f;
        addingSpeed = 3f;
        _tempPosPicture = transform.position;
        _img = GetComponent<Image>();
        _sc = swipeObj.GetComponent<SwipeController>();
        _newspap = player.GetComponent<NewspaperMovement2>();
        _move = player.GetComponent<PersonMovement>();
        points = 0;
        score = 0;
        _music = camera.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if(!_move.GetPlayerLife())
        {
            reasonForEnd = "Вас сбила машина";
        }
        if(_move.GetPlayerStatus())
        {
            reasonForEnd = "Вы не справились с работой";
        }
        _sc.SwipeEvent += CheckSwipe;// подписка на event

        if(_move.GetPlayerLife() && !_move.GetPlayerStatus()) // изменение очков
        {
            addingSpeed += 0.001f;
            addingSpeed = Mathf.Clamp(addingSpeed, 3f, 45f);
            points += Time.fixedDeltaTime * addingSpeed;
            runningScore.text = ((int)points).ToString();
        }
        else
        {
            runningScore.enabled = false; // если персонаж проиграл то очки прячутся 
        }
    }

    public float addSpeed()// добавление скорости игры
    {
        gameSpeed += 0.0003f;
        gameSpeed = Mathf.Clamp(gameSpeed, 10f, 22f);
        return gameSpeed;
    }

    public void showPicture(GameObject coll)// вывести картинку нужного свайпа
    {
        getPicture();
        _isPictureActive = true;
        _pictureIndex = Random.Range(0, pictures.Count);
        _img.sprite = pictures[_pictureIndex];
        var tempColor = _img.color;
        tempColor.a = 1f;
        _img.color = tempColor;
        _swipeCollider = coll;
    }
    public void hidePicture()
    {
        transform.position = _tempPosPicture;
        _isPictureActive = false;
        _isPirtureOnScreen = false;
        var tempColor = _img.color;
        tempColor.a = 0f;
        _img.color = tempColor;
    }
    private void CheckSwipe(SwipeController.SwipeType type) // проверка правильный свайп ли сделал игрок
    {
        if (_isPictureActive)
        {
            if (type == (SwipeController.SwipeType)thisType[_pictureIndex])
            {
                SwipeObject.swipeObject.ColliderOff(_swipeCollider);
                SwipeObject.swipeObject.SaveSwipeObject(_swipeCollider);
                score++;
                SwipeObject.swipeObject.ChangeSwipeColor(new Color(0, 1, 0, 0.5f));
                _newspap.CreateNewspaper();
            }
            else
            {
                SwipeObject.swipeObject.SaveSwipeObject(_swipeCollider);
                SwipeObject.swipeObject.ChangeSwipeColor(new Color(1, 0, 0, 0.5f));
            }
            newspaperScore.text = score.ToString();
            hidePicture();
        }
    }
    private void getPicture() // функция для вывода свайпа на нужной стороне экрана
    {
        if (!_isPirtureOnScreen)
        {
            if (_move.getLaneNumber() == 0)
            {
                transform.position = new Vector3(_tempPosPicture.x - 256f, _tempPosPicture.y, _tempPosPicture.z);
            }
            else if (_move.getLaneNumber() == 2)
            {
                transform.position = new Vector3(_tempPosPicture.x + 256f, _tempPosPicture.y, _tempPosPicture.z);
            }
        }
        _isPirtureOnScreen = true;
    }
   
    public void StartOrStorGame()
    {
        if(!paused)
        {
            Time.timeScale = 0;
            paused = true;
            _music.Stop();
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
            _music.Play();
        }
    }
   
}
