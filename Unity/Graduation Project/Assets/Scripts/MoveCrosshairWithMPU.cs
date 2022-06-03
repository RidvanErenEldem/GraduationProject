using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;

public class MoveCrosshairWithMPU : MonoBehaviour
{
    public string whichDuck;
    [SerializeField] public Animator duckAnim;
    public int ammo;
    private bool isClicking = false;
    public static string portName = "COM7";
    public static int baudRate = 115200;
    public float xSensitivity = 35;
    public float ySensitivity = 25;
    private float timer;

    SerialPort stream = new SerialPort(portName, baudRate);
    Rigidbody2D rb;
    Rigidbody2D duckRigidbody;
    private double normalizedXValue;
    private double normalizedYValue;
    //private bool isCalibrated = false;

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
        rb = GetComponent<Rigidbody2D>();
        duckRigidbody = GameObject.Find(whichDuck).GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)    {
        if (collision.gameObject.name == whichDuck)
        {
            if(isClicking)
            {
                timer += Time.deltaTime;
                duckAnim.SetBool("isDuckShooted", true);
                duckRigidbody.gravityScale = 0;
                duckRigidbody.velocity = new Vector2(0,0);
                if(timer >= 1f)
                {
                    duckRigidbody.gravityScale = 1;
                    duckRigidbody.velocity = new Vector2(0, -10f);
                    duckAnim.SetBool("isDuckDown", true);
                }
            Destroy(this.gameObject, 2f);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        string values = stream.ReadLine();
        string[] splitValues = values.Split(' ');


        normalizedYValue = Convert.ToDouble(splitValues[0]);
        normalizedXValue = Convert.ToDouble(splitValues[2]);
        normalizedXValue /= 180;
        normalizedYValue /= 180;

        normalizedXValue *= -xSensitivity;
        normalizedYValue *= ySensitivity;

        //Debug.Log("NormalizedXValue: " + normalizedXValue + " NormalizedYValue: " + normalizedYValue);

        transform.position = new Vector3((float)normalizedXValue, (float)normalizedYValue, -2f);

        if(splitValues[4] == "0")
        {
            if(ammo <= 0)
                Debug.Log("You Are Out of Ammo");
            else
            {
                isClicking = true;
            }
        }
        else
            isClicking = false;
        /*else
        {
            Debug.Log(values);
            if(values == "Unity Begin")
                isCalibrated = true;
        }*/


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
