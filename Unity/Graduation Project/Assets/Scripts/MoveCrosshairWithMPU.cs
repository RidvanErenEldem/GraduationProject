using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;
using TMPro;


public class MoveCrosshairWithMPU : MonoBehaviour
{
    public int ammoPerRount = 3;
    public string whichDuck;
    public string whichPlayer;
    private TextMeshProUGUI scoreGUI;
    private TextMeshProUGUI ammoGUI;
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
    private AudioSource audioSource;
    Rigidbody2D duckRigidbody;
    private double normalizedXValue;
    private double normalizedYValue;
    private float roundPoint = 500;
    public int currentPoint;
    //private bool isCalibrated = false;
    void Start()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        duckRigidbody = GameObject.Find(whichDuck).GetComponent<Rigidbody2D>();
        if(whichPlayer == "0")
        {
            ammoGUI = GameObject.Find("player1Ammo").GetComponent<TextMeshProUGUI>();
            scoreGUI = GameObject.Find("player1Score").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            ammoGUI = GameObject.Find("player2Ammo").GetComponent<TextMeshProUGUI>();
            scoreGUI = GameObject.Find("player2Score").GetComponent<TextMeshProUGUI>();
        }
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
            transform.position = new Vector3((float)normalizedXValue, (float)normalizedYValue, -3f);
            if (splitValues[4] == "0")
            {
                if (ammo <= 0)
                    Debug.Log("You are out of ammo!");
                else
                {
                    audioSource.Play();
                    ammo--;
                    int playerNum = Convert.ToInt32(whichPlayer) + 1;
                    ammoGUI.SetText($"PLAYER {playerNum} AMMO\n{ammo}/{ammoPerRount}");
                    if (isTriggering && !isDuckHit)
                    {
                        currentPoint += (int)Math.Round(roundPoint);
                        scoreGUI.SetText(currentPoint.ToString());
                        Debug.Log(currentPoint);
                        isDuckHit = true;
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
        }
        else
            roundPoint -= Time.deltaTime*0.1f*roundPoint;
    }
}
