using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float timer = 0f;
    private int roundCount = 0;
    private bool resetArduino = false;
    [SerializeField] public Animator greenDuckAnim;
    [SerializeField] public Animator redDuckAnim;
    private Image roundImage;
    private TextMeshProUGUI roundBaseText;
    private TextMeshProUGUI roundCounterText;
    public static bool pointReset = false;
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
        roundImage = GameObject.Find("RoundImage").GetComponent<Image>();
        roundBaseText = GameObject.Find("RoundBaseText").GetComponent<TextMeshProUGUI>();
        roundCounterText = GameObject.Find("RoundCounterText").GetComponent<TextMeshProUGUI>();
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
        Debug.Log(timer);

        roundImage.enabled = false;
        roundBaseText.enabled = false;
        roundCounterText.enabled = false;
        if ((greenDuckRigidbody.transform.position.y <= -5.87 || greenDuckRigidbody.transform.position.y >= 5.87)
            && (redDuckRigidbody.transform.position.y <= -5.87 || redDuckRigidbody.transform.position.y >= 5.87))
        {
            if (timer >= 2f)
            {
                roundCount++;
                float greenDuckXPosition = Random.Range(-9, 9);
                float redDuckXPosition = Random.Range(-9, 9);

                redDuckAnim.SetBool("isDuckRespawn", true);
                redDuckAnim.SetBool("isDuckShooted", false);

                greenDuckAnim.SetBool("isDuckRespawn", true);
                greenDuckAnim.SetBool("isDuckShooted", false);

                greenDuckRigidbody.transform.position = new Vector3(greenDuckXPosition, -1.89f, -1);
                redDuckRigidbody.transform.position = new Vector3(redDuckXPosition, -1.89f, -1);
                greenDuckRigidbody.gravityScale = 1.0f;
                redDuckRigidbody.gravityScale = 1.0f;
                pointReset = true;
                timer = 0f;
                resetArduino = true;
            }
            else
            {
                if(resetArduino)
                {
                    stream.Close();
                    stream.Open();
                }
                resetArduino = false;
                roundImage.enabled = true;
                roundBaseText.enabled = true;
                roundCounterText.enabled = true;
                roundCounterText.SetText(roundCount.ToString());
                timer += Time.deltaTime;
            }
        }
        else
            pointReset = false;

    }
}
