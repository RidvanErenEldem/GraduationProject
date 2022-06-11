using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int duckCounter = 0;
    public Rigidbody2D greenDuckRigidbody;
    public Rigidbody2D redDuckRigidbody;
    public MoveCrosshairWithMPU playerOne;
    public MoveCrosshairWithMPU playerTwo;
    public static string portName = "COM7";
    public static int baudRate = 115200;
    private AudioSource audioSource;
    SerialPort stream = new SerialPort(portName, baudRate);

    void Start()
    {
    }

    void Awake()
    {
        stream.Open();
    }

    //Closes stream so next time it doesn't create problems 
    void OnDestroy()
    {
        stream.Close();
    }

    // Update is called once per frame
    void Update()
    {
        string values = stream.ReadLine();
        playerOne.ReadTheData(values);
        playerTwo.ReadTheData(values);
        string[] splitValues = values.Split(' ');
        Debug.Log(duckCounter);
        if(duckCounter == 2)
        {
            float greenDuckXPosition = Random.Range(-9,9);
            float redDuckXPosition = Random.Range(-9,9);

            greenDuckRigidbody.transform.position = new Vector3(greenDuckXPosition, -1.89f,-1);
            redDuckRigidbody.transform.position = new Vector3(redDuckXPosition,-1.89f,-1);
            greenDuckRigidbody.gravityScale = 1.0f;
            redDuckRigidbody.gravityScale = 1.0f;
            duckCounter = 0;
        }
    }
}
