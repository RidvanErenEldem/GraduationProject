using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 mousePos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        var target = GameObject.Find("Crosshair");
        target.transform.position = new Vector3(mousePos.x, mousePos.y, -9);*/
        if(Input.GetMouseButtonDown(0))
        {
            source.Play();
        }
    }
}
