using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] public HighScore scores;
    private AudioSource gameOver;
    private TextMeshProUGUI player1Score;
    private TextMeshProUGUI player2Score;
    private TextMeshProUGUI player1Base;
    private TextMeshProUGUI player2Base;
    private TextMeshProUGUI chickenDinner;
    private TextMeshProUGUI whoIsDinner;
    private TextMeshProUGUI returnToTheDarkness;
    private AudioSource drumRoll;
    private float timer = 0.0f;
    private List<GameObject> objects;
    private int highScore;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = GameObject.Find("gameOver").GetComponent<AudioSource>();
        drumRoll = GameObject.Find("drumRoll").GetComponent<AudioSource>();
        player1Score = GameObject.Find("player1Score").GetComponent<TextMeshProUGUI>();
        player2Score = GameObject.Find("player2Score").GetComponent<TextMeshProUGUI>();
        player1Base = GameObject.Find("player1Base").GetComponent<TextMeshProUGUI>();
        player2Base = GameObject.Find("player2Base").GetComponent<TextMeshProUGUI>();
        chickenDinner = GameObject.Find("chickenDinner").GetComponent<TextMeshProUGUI>();
        whoIsDinner = GameObject.Find("whoIsDinner").GetComponent<TextMeshProUGUI>();
        returnToTheDarkness = GameObject.Find("returnToTheDarkness").GetComponent<TextMeshProUGUI>();

        player1Score.SetText($"<align=\"center\">{scores.player1Score.ToString()}</align>");
        player2Score.SetText($"<align=\"center\">{scores.player2Score.ToString()}</align>");
        if (scores.player1Score > scores.player2Score)
        {
            highScore = scores.player1Score;
            whoIsDinner.SetText($"<align=\"center\">PLAYER ONE</align>");
        }
        else
        {
            highScore = scores.player2Score;
            whoIsDinner.SetText($"<align=\"center\">PLAYER TWO</align>");
        }
            
        drumRoll.Play();

        player1Score.enabled = false;
        player2Score.enabled = false;
        player1Base.enabled = false;
        player2Base.enabled = false;
        whoIsDinner.enabled = false;
        returnToTheDarkness.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 5.825f)
        {
            player1Base.enabled = true;
            player1Score.enabled = true;
        }
        if (timer >= 11.65f)
        {
            player2Base.enabled = true;
            player2Score.enabled = true;
            whoIsDinner.enabled = true;
        }
        if (timer >= 12.65f)
        {
            returnToTheDarkness.enabled = true;
        }
        if (timer >= 13.15f)
        {
            returnToTheDarkness.enabled = false;
            timer = 12f;
        }
        if (!drumRoll.isPlaying)
        {
            if (!gameOver.isPlaying)
                gameOver.Play();
        }
        if (Input.anyKeyDown)
        {
            try
            {
                int storedHighScore;
                using (StreamReader sr = new StreamReader("HighScore.txt"))
                {
                    string line = sr.ReadLine();
                    storedHighScore = Convert.ToInt32(line);
                }

                if(highScore > storedHighScore)
                {
                    using (StreamWriter sw = new StreamWriter("HighScore.txt"))
                    {
                        sw.Write(highScore.ToString());
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            SceneManager.LoadScene("TitleScreen");
        }

        timer += Time.deltaTime;
    }
}
