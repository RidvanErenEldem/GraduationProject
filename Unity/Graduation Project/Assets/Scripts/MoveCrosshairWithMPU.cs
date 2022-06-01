using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;

public class MoveCrosshairWithMPU : MonoBehaviour
{
    private int min;
    private int max;

    readonly static string portName = "COM7";
    readonly static int baudRate = 115200;
    SerialPort stream = new SerialPort(portName, baudRate);
    Rigidbody2D rb;
    void Awake()
    {
        stream.Open();
    }

    //Closes stream so next time it doesn't create problems 
    void OnDestroy()
    {
        stream.Close();
    }
    void Start()
    {
        min = -180;
        max = 180;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        string values = stream.ReadLine();
        string[] splitValues = values.Split(' ');

        foreach (string value in splitValues)
        {
            Debug.Log(value);
        }

        // string message = serialController.ReadSerialMessage();

        // if (message == null)
        //     return;

        // string[] separatedMessages = message.Split(' ');
        // xPosition = Convert.ToInt32(separatedMessages[0]);
        // yPosition = Convert.ToInt32(separatedMessages[2]);
        // if(separatedMessages[3] == "1")
        //     isClickedButton1 = false;
        // else
        //     isClickedButton1 = true;
        
        // if(separatedMessages[4] == "1")
        //     isClickedButton2 = false;
        // else
        //     isClickedButton1 = true;
        

       
        
    }
}
