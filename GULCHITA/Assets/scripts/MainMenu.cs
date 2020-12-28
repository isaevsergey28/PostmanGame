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

    private string roadName;
    static public int roadNumber;
    private bool _isPressed = false;

    private const int port = 1337;
    private const string server = "127.0.0.1";

    void Start()
    {
        roadName = string.Empty;
    }
    
    
    void Update()
    {
        
        if(_isPressed)
        {
            _isPressed = false;
            ConnectToServer();
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
}
