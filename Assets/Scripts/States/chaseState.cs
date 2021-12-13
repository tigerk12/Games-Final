using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseState : State
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Transform spawn;

    public attackState attack;
    public idleState idle;
    public static bool inAttack;
    public bool lost;
    public bool returned;
    public Animator animator;
    public float speed;
    public float agroRange;
    public float attackRange;
    public GameObject AI;
    bool change = true;

    Rigidbody2D rb;

    void Start()
    {
        rb = AI.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);
        if(distToPlayer < agroRange) //if player is within range, chase
        {
            ChasePlayer();
        }
        else 
        {
            StopChase();
        }


        if (distToPlayer < attackRange) //if player is within range, attack
        {
            inAttack = true;
            animator.SetBool("walk", false);

        }


    }

    public override State RunCurrentState()
    {
        

        if (inAttack)
        {
            return attack;
        }
        else if (lost)
        {
            goBack();
        }
        else if (returned)
        {
            return idle;
        }

        return this;
    }

    void ChasePlayer()
    {
        animator.SetBool("idle", false);
        animator.SetBool("walk", true);
        lost = false;

        if (transform.position.x < target.position.x)
        {
            flip();
            rb.velocity = new Vector2(speed, 0);

            change = false;
        }

        else if (transform.position.x > target.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            if (!change)
            {
                change = true;
                flip();
            }
        }
    }

    void StopChase() //if player is out of range, stop chasing
    {
        animator.SetBool("walk", false);
        animator.SetBool("idle", true);
        lost = true;
        rb.velocity = Vector2.zero;
    }

    void flip()
    {
        if (change)
        {
            AI.transform.Rotate(0.0f, 180f, 0);
        }
    }

    void goBack() //go back to spawn
    {
        float distToSpawn = Vector2.Distance(transform.position, spawn.position);
        if (distToSpawn < attackRange) 
        {
            animator.SetBool("walk", true);
            animator.SetBool("idle", false);

            if (transform.position.x < spawn.position.x)
            {
                flip();
                rb.velocity = new Vector2(speed, 0);

                change = false;
            }

            else if (transform.position.x > spawn.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);
                if (!change)
                {
                    change = true;
                    flip();
                }
            }
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
            returned = true;
        }
    }

}
