using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;

public class MoveCrosshairWithMPU : MonoBehaviour
{
    public string whichDuck;
    public string whichPlayer;
    [SerializeField] public Animator duckAnim;
    public int ammo;
    private bool isTriggering = false;
    private bool isDuckHit = false;
    public static string portName = "COM7";
    public static int baudRate = 115200;
    public float xSensitivity = 35;
    public float ySensitivity = 25;
    private float timer;
    Rigidbody2D rb;
    Rigidbody2D duckRigidbody;
    private double normalizedXValue;
    private double normalizedYValue;
    //private bool isCalibrated = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        duckRigidbody = GameObject.Find(whichDuck).GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == whichDuck)
        {
            isTriggering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == whichDuck)
        {
            isTriggering = false;
        }
    }

    public void ReadTheData(string data)
    {
        string[] splitValues = data.Split(' ');
        //Debug.Log("NormalizedXValue: " + normalizedXValue + " NormalizedYValue: " + normalizedYValue);
        if (splitValues[0] == whichPlayer)
        {
            normalizedYValue = Convert.ToDouble(splitValues[1]);
            normalizedXValue = Convert.ToDouble(splitValues[3]);
            normalizedXValue /= 180;
            normalizedYValue /= 180;

            normalizedXValue *= -xSensitivity;
            normalizedYValue *= ySensitivity;
            transform.position = new Vector3((float)normalizedXValue, (float)normalizedYValue, -2f);
            if (splitValues[4] == "0")
            {
                if (ammo <= 0)
                    Debug.Log("You are out of ammo!");
                else
                {
                    ammo--;
                    if (isTriggering)
                    {
                        Debug.Log("Duck Hit!");
                        isDuckHit = true;
                    }
                    else
                    {
                        Debug.Log("Duck Miss");
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDuckHit)
        {
            timer += Time.deltaTime;
            duckAnim.SetBool("isDuckShooted", true);
            duckRigidbody.gravityScale = 0;
            duckRigidbody.velocity = new Vector2(0, 0);
            if (timer >= 1f)
            {
                duckRigidbody.gravityScale = 1;
                duckRigidbody.velocity = new Vector2(0, -10f);
                duckAnim.SetBool("isDuckDown", true);
                isDuckHit = false;
            }
            Destroy(GameObject.Find(whichDuck), 2f);
        }
    }
}
