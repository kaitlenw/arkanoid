using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;

    public float minBound;
    public float maxBound;
    public bool overrideBounds = false;
    
    private float halfWidth;
    void Start()
    {
        if (!overrideBounds)
        {
            float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
            minBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,camDistance)).x;
            maxBound = Camera.main.ViewportToWorldPoint(new Vector3(1,1,camDistance)).x;
        }
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x/2;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(x*speed, 0, 0);
        if ((newPosition.x > minBound + halfWidth) && (newPosition.x < maxBound - halfWidth))
        {
            rb.MovePosition(newPosition);
        }
    }
}
