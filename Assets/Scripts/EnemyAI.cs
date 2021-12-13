using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public Transform AI;
    public float speed = 300f;
    public float WaypointDist = 3f;
    public Animator animator;
    public GameObject enemy;
    public Transform moveSpot;
    public static bool openGate = false;
    public static bool goBack = false;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    //path is always updated
    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    //When AI reaches target, waypoint = 0
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Add force to AI so it flies towards the player with some speed
    void FixedUpdate()
    {
        if (openGate)
        {
            animator.SetBool("Fly", true);

            if (path == null)
            {
                return;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            if (!goBack)
                rb.AddForce(force);
            else
                rb.AddForce(-force);

            float distcance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distcance < WaypointDist)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f)
            {
                AI.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (force.x <= 0.01f)
            {
                AI.localScale = new Vector3(1f, 1f, 1f);
            }


        } 
    }
}
