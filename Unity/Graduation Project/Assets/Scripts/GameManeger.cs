using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public Rigidbody2D greenDuckRigidbody;
    public Rigidbody2D redDuckRigidbody;
    void Start()
    {
        greenDuckRigidbody = GameObject.Find("GreenDuck").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
