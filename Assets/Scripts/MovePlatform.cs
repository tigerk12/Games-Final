using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    public GameObject floor;
    public Transform moveSpot;
    public float speed;

    public static bool move = false;


    // Update is called once per frame
    void Update()
    {
        if (move) //move floor to specified points
        {
            floor.transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        } 

    }
        
}
