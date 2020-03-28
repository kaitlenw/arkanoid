using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{

    public List<GameObject> bricks;
    // Start is called before the first frame update
    void Start()
    {
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool RemoveBrick(GameObject brick)
    {
        bricks.Remove(brick);
        return bricks.Count == 0;
    }
}
