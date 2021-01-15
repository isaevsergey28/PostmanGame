using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameRestarter : MonoBehaviour
{
    public GameObject player;
    private PersonMovement _playerMove;
    public GameObject restartPanel;
    public GameObject arrowsButton;
   

    private bool _isOpen = false;

    private void Start()
    {
        _playerMove = player.GetComponent<PersonMovement>();
    }
    private void Update()
    {
        if (!_isOpen)
        {
            if (!_playerMove.GetPlayerLife())//если пользователь проиграл спрятать управление и вывести меню рестарта игры
            {
                restartPanel.SetActive(true);
                arrowsButton.SetActive(false);
                _isOpen = true;
            }
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToMainMenu()
    {
        int newspaperScore;
        int runningScore;
        if (PlayerPrefs.HasKey("newspaperScore"))
        {
            newspaperScore = PlayerPrefs.GetInt("newspaperScore");
        }
        else
        {
            newspaperScore = 0;
        }
        if (PlayerPrefs.HasKey("runningScore"))
        {
            runningScore = PlayerPrefs.GetInt("runningScore");
        }
        else
        {
            runningScore = 0;
        }
        if(newspaperScore < GameController.gameController.score)
        {
            newspaperScore = GameController.gameController.score;
            PlayerPrefs.SetInt("newspaperScore", newspaperScore);
        }
        if (runningScore < (int)GameController.gameController.points)
        {
            runningScore = (int)GameController.gameController.points;
            PlayerPrefs.SetInt("runningScore", runningScore);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
