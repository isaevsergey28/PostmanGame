using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public AudioClip maksim;
    public GameObject menu;
    public GameObject instruction;

    public Text newspaperScore;
    public Text runningScore;

    private string roadName;
    static public int roadNumber;
    private bool _isPressed = false;

    private const int port = 1337;
    private const string server = "127.0.0.1";

    private int _newspaperScore;
    private int _runningScore;

    private AudioSource _aus;

    void Start()
    {
        roadName = string.Empty;

        if(PlayerPrefs.HasKey("newspaperScore"))
        {
            _newspaperScore = PlayerPrefs.GetInt("newspaperScore");
        }
        else
        {
            _newspaperScore = 0;
        }
        if (PlayerPrefs.HasKey("runningScore"))
        {
            _runningScore = PlayerPrefs.GetInt("runningScore");
        }
        else
        {
            _runningScore = 0;
        }
        newspaperScore.text += " " + _newspaperScore.ToString();
        runningScore.text += " " + _runningScore.ToString();


        _aus = GetComponent<AudioSource>();
        _aus.clip = maksim;
    }
    
    
    void Update()
    {
        
        if(_isPressed)
        {
            //_isPressed = false;
            //ConnectToServer();
            RestartLevel();
        }
    }

    private void ConnectToServer() // соединение с сервером
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect(server, port);
            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            NetworkStream stream = client.GetStream();

            data = Encoding.UTF8.GetBytes(roadName);
            stream.Write(data, 0, data.Length);

            do
            {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            roadNumber = Convert.ToInt32(response.ToString());
            stream.Close();
            client.Close();
        }
        finally
        {
            RestartLevel();
        }

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void GetButtonName() // функция определения какая кнопка из мен была нажата
    {
        _isPressed = true;
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName = selectedButton.transform.GetChild(0).name.ToString();
        roadName = buttonName;
    }

    public void SeeInstructions()
    {
        menu.SetActive(false);
        instruction.SetActive(true);
        
        _aus.Play();
    }
    public void BackToMenu()
    {
        menu.SetActive(true);
        instruction.SetActive(false );
        _aus.Stop();
    }
}
