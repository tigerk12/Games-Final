using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBomb : MonoBehaviour
{
    //public Animator animator;
    public Animator animator;
    public Animator bossAnim;
    public GameObject[] bomb;
    public GameObject boss;
    public GameObject door;
    private int bombNum = 3;


    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
    }

    public void shoot() //shoot bomb at boss
    {
        animator.SetTrigger("fire");
        bombNum = bombNum - 1;
        bossAnim.SetTrigger("damage");

        if (bombNum < 2)
            Destroy(bomb[bombNum].gameObject);

        if (bombNum == 0)
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End() //after final shot is fired, door appears
    {
        yield return new WaitForSeconds(2);
        boss.SetActive(false);
        door.SetActive(true);
    }
      
}
