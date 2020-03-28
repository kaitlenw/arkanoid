using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    public float speed;
    public int startLives;
    private int livesLeft;
    private Rigidbody2D rb;
    private bool gameHasStarted = false;
    private Vector2 startPos;
    public Sprite sprite; 
    public GameObject lifeDisplay;
    public GameObject winPanel;
    public GameObject gameOverPanel;

    private AudioSource source;
    public AudioClip brickSFX;
    public AudioClip paddleSFX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        livesLeft = startLives;
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((!gameHasStarted) && Input.GetButton("Jump") && (livesLeft >= 0))
        {
            rb.velocity = (Vector2.up + new Vector2(Random.Range(-0.50f, 0.5f), 0)) * speed;
            gameHasStarted = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Floor")
        {
            gameHasStarted = false;
            rb.velocity = Vector2.zero;
            transform.position = startPos;
            livesLeft--;
            if (livesLeft < 0)
            {
                gameOverPanel.SetActive(true);
            }
        }
        else 
        {
            if (col.gameObject.tag == "Paddle") 
            {
                source.PlayOneShot(paddleSFX);
            }
            if (col.gameObject.tag == "Brick")
            {
                source.PlayOneShot(brickSFX);
                bool hasWon = col.gameObject.transform.parent.GetComponent<Bricks>().RemoveBrick(col.gameObject);
                Destroy(col.gameObject);
                if (hasWon)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = startPos;
                    winPanel.SetActive(true);
                    return;
                }
            }
            rb.velocity = Vector3.Reflect(rb.velocity, col.GetContact(0).normal);
        }
    }

    void OnGUI()
    {
        foreach (Transform child in lifeDisplay.transform)
        {
            Destroy(child.gameObject);
        }
        for (int x = 0; x < startLives; x++)
        {
            GameObject life = new GameObject();
            Image img = life.AddComponent<Image>();
            img.preserveAspect = true;
            img.sprite = sprite;
            RectTransform rt = life.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(70, 70);
            // draw image in box
            if (x >= livesLeft)
            {
                img.enabled = false;
            }
            rt.SetParent(lifeDisplay.transform);
        }
    }
}
