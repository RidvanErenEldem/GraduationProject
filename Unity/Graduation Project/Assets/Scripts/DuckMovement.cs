using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int howManyTimesDoWeHaveToTeachYouThisLessonOldMan = 0;
    private int borderCounter;
    [SerializeField] private Animator anim;
    private float timer;
    public bool duckShooted = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        if(duckShooted)
            Debug.Log("Duck Hit Successful!");
        else
            Debug.Log("Duck Flew Away!");
    }
    // Update is called once per frame
    void Update()
    {
        
        float xPosition = Random.Range(-9,9);
        float yVelocity = Random.Range(7,12);
        float xVelocity = Random.Range(-4, 4);
        
        
        if(this.transform.position.x <= -9.19f)
        {
            if(xVelocity <= 0)
                rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }
        if(this.transform.position.x >= 9.19f)
        {
            if(xVelocity >= 0)
               rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }
        
        if(this.transform.position.y <= -2 && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("DuckDown"))
        {
            if(borderCounter == howManyTimesDoWeHaveToTeachYouThisLessonOldMan)
            {
                this.transform.position = new Vector3(rb.position.x, -1.95f, -1);
                rb.velocity = new Vector2(0f, 15f);
                Destroy(this.gameObject, 0.9f);
            }
            else
            {
                yVelocity = Random.Range(7,12);
                xVelocity = Random.Range(-4, 4);

                this.transform.position = new Vector3(rb.position.x, -1.95f, -1);
                rb.velocity = new Vector2(xVelocity, yVelocity);
            }
            borderCounter++;
        }
    }
}
