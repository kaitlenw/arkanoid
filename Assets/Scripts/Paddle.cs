using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;

    private Vector2 minBound;
    private Vector2 maxBound;

    private float halfWidth;
    void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        minBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,camDistance));
        maxBound = Camera.main.ViewportToWorldPoint(new Vector3(1,1,camDistance));
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x/2;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(x*speed, 0, 0);
        if ((newPosition.x > minBound.x + halfWidth) && (newPosition.x < maxBound.x - halfWidth))
        {
            rb.MovePosition(newPosition);
        }
    }
}
