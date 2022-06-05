using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public Rigidbody2D greenDuckRigidbody;
    public Rigidbody2D redDuckRigidbody;
    public MoveCrosshairWithMPU playerOne;
    public MoveCrosshairWithMPU playerTwo;
    public static string portName = "COM7";
    public static int baudRate = 115200;
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
    }
}
