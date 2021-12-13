using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject[] units;
    public int numberOfEnemies = 10;
    public Vector3 range = new Vector3(5, 5, 5);
    public bool seekGoal = true;
    public bool obedient = true;
    public bool willful = false;
    public int moveSpeed;
    public static bool spawn = false;
    bool start;
    Rigidbody2D rb;

    [Range(0, 200)]
    public int neighbourDistance = 50;
    [Range(0, 2)]
    public float maxForce = 0.5f;
    [Range(0, 5)]
    public float maxVelocity = 2f;


    // Spawn flock of enemies up to number of units
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        units = new GameObject[numberOfEnemies];
    }

    void Update()
    {
        if (start)
        {
            rb.velocity = Vector2.up * -moveSpeed;
        }
    }


    public void Spawn()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0);
            units[i] = Instantiate(enemyPrefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i].GetComponent<Enemy>().manager = this.gameObject;
        }
        start = true;
        
    }
}
