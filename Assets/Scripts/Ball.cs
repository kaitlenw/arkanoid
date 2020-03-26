using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (Vector2.up + new Vector2(Random.Range(-0.50f, 0.5f), 0)) * speed;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        // Hit the Racket?
        if (col.gameObject.tag == "Paddle") 
        {
            // Calculate hit Factor
            float x=hitFactor(transform.position,col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed;
        }
        else 
        {
            if (col.gameObject.tag == "Brick")
            {
                Destroy(col.gameObject);
            }
            rb.velocity = Vector3.Reflect(rb.velocity, col.GetContact(0).normal);
        }
    }
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) 
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }
}
