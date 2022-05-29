using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPosition = Random.Range(-9,9);

        if(this.transform.position.y < -1.5)
        {
            xPosition = Random.Range(-9,9);
            this.transform.position = new Vector3(xPosition, -1, -1);

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }
    }
}
