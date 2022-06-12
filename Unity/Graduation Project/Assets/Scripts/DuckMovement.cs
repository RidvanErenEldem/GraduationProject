using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public bool runOnce = true;
    public int howManyTimesDoWeHaveToTeachYouThisLessonOldMan = 0;
    public int borderCounter;
    [SerializeField] private Animator anim;
    private float timer;
    public bool duckShooted = false;
    private float randomYVelocity;
    private float randomXVelocity;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x <= -9.19f)
        {
            if(rb.velocity.x <= 0)
                rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }
        if(this.transform.position.x >= 9.19f)
        {
            if(rb.velocity.x >= 0)
               rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
        }
        
        if(this.transform.position.y <= -2 && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("DuckDown"))
        {
            if(borderCounter == howManyTimesDoWeHaveToTeachYouThisLessonOldMan)
            {
                this.transform.position = new Vector3(rb.position.x, -1.95f, -1);
                rb.velocity = new Vector2(0f, 15f);
                borderCounter = 0;
            }
            else
            {
                randomYVelocity = Random.Range(7,12);
                randomXVelocity = Random.Range(-5, 5);

                this.transform.position = new Vector3(rb.position.x, -1.95f, -1);
                rb.velocity = new Vector2(randomXVelocity, randomYVelocity);
                borderCounter++;
            }
        }
        else if(this.transform.position.y <= -5.89 || this.transform.position.y >= 5.89)
        {
            borderCounter = 0;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
