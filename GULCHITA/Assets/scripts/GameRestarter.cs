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
        SceneManager.LoadScene("MainMenu");
    }
}
