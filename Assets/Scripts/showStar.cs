using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showStar : MonoBehaviour
{
    public GameObject[] goldStar;
    public GameObject[] noStar;
    public GameObject life3;
    public GameObject life4;
    public GameObject life5;
    public Animator animator;
    public AudioPlayer AP;

    public static int level;
    public static bool first;
    public static bool second;
    public static bool third;
    public static bool fourth;

    // Start is called before the first frame update
    void Start()
    {
        goldStar[0].SetActive(false);
        goldStar[1].SetActive(false);
        goldStar[2].SetActive(false);
        goldStar[3].SetActive(false);
        noStar[0].SetActive(false);
        noStar[1].SetActive(false);
        noStar[2].SetActive(false);
        noStar[3].SetActive(false);
        life4.SetActive(false);
        life5.SetActive(false);


        if (first) //if star has been collected, player is rewarded with an extra life
        {
            goldStar[0].SetActive(true);
            AP.GetComponent<AudioPlayer>().IncHeart();
            life3.SetActive(false);
            life4.SetActive(true);
            kill.maxLife = 4;

            if(level == 1)
            {
                animator.SetTrigger("heart");
                AP.GetComponent<AudioPlayer>().IncHeart();
            }
        }

        if (!first && level > 0)  //player did not collect a star
        {
            noStar[0].SetActive(true);
        }

        if (second)
        {
            goldStar[1].SetActive(true);
            if (level == 2)
            {
                animator.SetTrigger("heart2");
                AP.GetComponent<AudioPlayer>().IncHeart();
            }

            if (first)
            {
                life4.SetActive(false);
                life5.SetActive(true);
                kill.maxLife = 5;
            }
            else
            {
                life3.SetActive(false);
                life4.SetActive(true);
                kill.maxLife = 4;

            }

        }

        if (!second && level > 1)
        {
            noStar[1].SetActive(true);
        }

        if (third && level > 2)
        {
            goldStar[2].SetActive(true);
            if (level == 3)
            {
                animator.SetTrigger("heart3");
                AP.GetComponent<AudioPlayer>().IncHeart();
            }
           
            if (kill.maxLife == 4)
            {
                life4.SetActive(false);
                life5.SetActive(true);
                kill.maxLife = 5;
            }
            else if (kill.maxLife == 3)
            {
                life3.SetActive(false);
                life4.SetActive(true);
                kill.maxLife = 4;
            }

        }

        if (!third && level > 2)
        {
            noStar[2].SetActive(true);
        }

        if (fourth && level > 3)
        {
            goldStar[3].SetActive(true);
            if (level == 3)
            {
                animator.SetTrigger("heart4");
                AP.GetComponent<AudioPlayer>().IncHeart();
            }

            if (kill.maxLife == 4)
            {
                life4.SetActive(false);
                life5.SetActive(true);
                kill.maxLife = 5;
            }
            else if (kill.maxLife == 3)
            {
                life3.SetActive(false);
                life4.SetActive(true);
                kill.maxLife = 4;
            }
        }

        if (!fourth && level > 3)
        {
            noStar[3].SetActive(true);
        }
    
    }
        
}
