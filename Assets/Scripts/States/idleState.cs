using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : State
{
    [SerializeField]
    Transform target;

    public Transform[] moveSpot;

    public chaseState chaseState;
    public bool canSeePlayer;
    public float agroRange;
    public float attackRange;
    private int randSec;
    private int randNum;
    public float speed;
    public GameObject AI;

    public Animator animator;

    private float waitTime;
    private float startWaitTime = 4;
    bool startMove = false;

    Rigidbody2D rb;

    void Start()
    {
        rb = AI.GetComponent<Rigidbody2D>();
        randNum = Random.Range(0, 2);
        randSec = Random.Range(1, 10);
        waitTime = startWaitTime;
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position); //if player is within range, return chaseState
        if (distToPlayer < agroRange)
        {
            canSeePlayer = true;
        } 
        else if (distToPlayer > agroRange)
        {
            canSeePlayer = false;

        }


        if (startMove)
        {
            AI.transform.position = Vector2.MoveTowards(transform.position, moveSpot[randNum].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot[randNum].position) < 0.2f)             
            {
                animator.SetBool("walk", false);
                animator.SetBool("idle", true);

                if (waitTime <= 0)
                {
                    randNum = Random.Range(0, 3);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else
            {
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
            }
        
        }

    }

    public override State RunCurrentState()
    {
        if (canSeePlayer)
        {
            return chaseState;
        }
        else
        {
            StartCoroutine(Wait());
        }
        return this;
    }

    IEnumerator Wait() //AI wanders off after a random amount of time
    {
        yield return new WaitForSeconds(randSec);
        startMove = true;

    }

}
