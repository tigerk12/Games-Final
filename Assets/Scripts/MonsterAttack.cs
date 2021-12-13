using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAttack : MonoBehaviour
{
    public Animator animator;
    private int objNum;
    private bool attacked;

    void OnTriggerEnter2D(Collider2D collision)
    {
        attacked = false;

        if (collision.gameObject.tag == "Player")
        {
            if(!attacked)
            {
                objNum = Random.Range(0, 2);
                if (objNum == 0)
                {
                    //fireball
                    animator.SetTrigger("attack01");
                    attacked = true;
                }
                if (objNum == 1)
                {
                    //failed attack
                    animator.SetTrigger("attack02");
                    attacked = true;
                }
            }

        }
    }
}
