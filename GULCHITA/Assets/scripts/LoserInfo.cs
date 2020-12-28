using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoserInfo : MonoBehaviour
{
    //вывод информации когда пользователь проиграл
    public GameObject controllerObject;
    private GameController _gm;

    private Text _playerInfo;

    void Start()
    {
        _gm = controllerObject.GetComponent<GameController>();
        _playerInfo = GetComponent<Text>();
        _playerInfo.text = _gm.reasonForEnd + "\n\nЗаброшенные газеты : " + _gm.score.ToString() + "\n\n" + "Ваши очки : " + ((int)_gm.points);
    }
}
