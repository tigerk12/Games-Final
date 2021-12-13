using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : State
{
    [SerializeField]
    Transform target;

    public Animator animator;
    private int objNum;
    private bool attacked;
    public chaseState chase;
    public float attackRange;

    bool lost;
    bool contact;

    void Start()
    {

    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);

        if (attackRange < distToPlayer && contact) //lose sight of player
        {
            lost = true;
        }
    }

    public override State RunCurrentState()
    {
        if (!attacked)
        {
            objNum = Random.Range(0, 2); //random attack
            if (objNum == 0)
            {
                //fireball shoots out of wand
                animator.SetTrigger("attack01");
                attacked = true;
                contact = true;
                StartCoroutine(coolDown());
            }
            if (objNum == 1)
            {
                //failed attack no fireball comes out
                animator.SetTrigger("attack02");
                attacked = true;
                contact = true;
                StartCoroutine(coolDown());
            }


        }

        if (lost)
        {
            chaseState.inAttack = false;
            return chase;
        }

        return this;
    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(2);
        attacked = false;
    }


}
