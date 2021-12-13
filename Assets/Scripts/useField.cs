using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useField : MonoBehaviour
{

    public Animator animator;
    public GameObject forceField;

    bool exited = false;


    void OnTriggerEnter2D(Collider2D collision) //When using forcefield, pathfinding AI, fly away from the player
    {
        if (collision.gameObject.tag == "Player" && exited)
        {
            forceField.gameObject.SetActive(true);
            animator.SetTrigger("inside");
            EnemyAI.goBack = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        forceField.gameObject.SetActive(false);
        EnemyAI.goBack = false;
        exited = true;
    }

}
