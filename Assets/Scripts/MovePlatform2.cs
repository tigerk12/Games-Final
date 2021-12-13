using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform2 : MonoBehaviour
{

    public GameObject floor;
    public Transform moveSpot;
    public float speed;

    public static bool move2 = false;

    // Update is called once per frame
    void Update()
    {
        if (move2)
        {
            floor.transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        }
    }
}
