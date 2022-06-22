using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;
using TMPro;


public class MoveCrosshairWithMPU : MonoBehaviour
{
    public int ammoPerRound = 3;
    public int maxPointPerRound = 700;
    public string whichDuck;
    public string whichPlayer;
    private TextMeshProUGUI scoreGUI;
    private TextMeshProUGUI ammoGUI;
    [SerializeField] public Animator duckAnim;
    public int ammo;
    private bool isTriggering = false;
    private bool isDuckHit = false;
    public float xSensitivity = 35;
    public float ySensitivity = 25;
    private float timer;
    Rigidbody2D rb;
    private AudioSource gunShot;
    private AudioSource gunEmpty;
    Rigidbody2D duckRigidbody;
    private double normalizedXValue;
    private double normalizedYValue;
    private float roundPoint;
    public int currentPoint;
    void Start()
    {
        roundPoint = maxPointPerRound;
        gunShot = GameObject.Find("GunShot").GetComponent<AudioSource>();
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
        gunEmpty = GameObject.Find("EmptyGun").GetComponent<AudioSource>();
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
                duckAnim.SetBool("isDuckRespawn", false);
                duckAnim.SetBool("isDuckShooted", false);
                duckAnim.SetBool("isDuckDown", false);
                if (ammo <= 0)
                    gunEmpty.Play();
                else
                {
                    gunShot.Play();
                    ammo--;
                    int playerNum = Convert.ToInt32(whichPlayer) + 1;
                    ammoGUI.SetText($"PLAYER {playerNum} AMMO\n{ammo}/{ammoPerRound}");
                    if (isTriggering && duckAnim.GetCurrentAnimatorStateInfo(0).IsName("DuckIdle") && !isDuckHit)
                    {
                        currentPoint += (int)Math.Round(roundPoint);
                        scoreGUI.SetText(currentPoint.ToString());
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
                duckAnim.SetBool("isDuckShooted", false);
                timer = 0f;
                isDuckHit = false;
            }
        }
        else if(!GameManager.pointReset)
            roundPoint -= Time.deltaTime*0.1f*roundPoint;
        else
        {
            roundPoint = maxPointPerRound;
            ammo = ammoPerRound;
            int playerNum = Convert.ToInt32(whichPlayer) + 1;
            ammoGUI.SetText($"PLAYER {playerNum} AMMO\n{ammo}/{ammoPerRound}");
        }
    }
}
