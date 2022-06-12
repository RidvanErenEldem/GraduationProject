using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    private TextMeshProUGUI timeToPlay;
    private TextMeshProUGUI HighScoreText;
    private float timer = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        timeToPlay = GameObject.Find("PlayText").GetComponent<TextMeshProUGUI>();
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        try
        {
            using (StreamReader sr = new StreamReader("HighScore.txt"))
            {
                string line = sr.ReadLine();
                HighScoreText.SetText(line);
            }
        }catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.5)
        {
            timeToPlay.enabled = true;
        }
        if (timer >= 1)
        {
            timeToPlay.enabled = false;
            timer = 0;
        }
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
