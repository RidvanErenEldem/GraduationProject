using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int howManyTimesDoWeHaveToTeachYouThisLessonOldMan = 0;
    private int borderCounter;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    private void OnMouseOver() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xPosition = Random.Range(-9,9);
        float yVelocity = Random.Range(7,12);
        float xVelocity = Random.Range(-4, 4);
        
        
        if(this.transform.position.x <= -9.18f)
        {
            if(xVelocity <= 0)
                rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }
        if(this.transform.position.x >= 9.18f)
        {
            if(xVelocity >= 0)
               rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }

        if(this.transform.position.y <= -1.5)
        {
            if(borderCounter == howManyTimesDoWeHaveToTeachYouThisLessonOldMan)
            {
                this.transform.position = new Vector3(rb.position.x, -1.4f, -1);
                rb.velocity = new Vector2(0f, 15f);
                Destroy(this.gameObject, 0.8f);
            }
            else
            {
                yVelocity = Random.Range(7,12);
                xVelocity = Random.Range(-4, 4);

                this.transform.position = new Vector3(rb.position.x, -1.4f, -1);
                rb.velocity = new Vector2(xVelocity, yVelocity);
            }
            borderCounter++;
        }
    }
}
