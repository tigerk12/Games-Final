using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    public Animator animator;
    private bool attacked;
    private int objNum;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (!attacked)
            {
                objNum = Random.Range(0, 2); //random attack
                StartCoroutine(attack());
                attacked = true;
            }

        }
    }

    IEnumerator attack() //2 different attack modes, 1 covers lower button and 2 covers the top 2 buttons.
    {
        if (objNum == 0)
        {
            animator.SetTrigger("attack");
            yield return new WaitForSeconds(2);
            attacked = false;

        }
        if (objNum == 1)
        {
            animator.SetTrigger("attack2");
            yield return new WaitForSeconds(2);
            attacked = false;
        }

    }
}
