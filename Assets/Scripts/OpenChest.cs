using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public GameObject Chest;
    public Animator animator;
    public GameObject game_object;
    public AudioPlayer AP;

    private player_movement Pm;
    private int objNum;
    bool open = false;

    public GameObject jump;
    public GameObject gravity;
    public GameObject jet;


    // Start is called before the first frame update
    void Start()
    {
        Chest.SetActive(true);
        jet.gameObject.SetActive(false);
        jump.gameObject.SetActive(false);
        gravity.gameObject.SetActive(false);

        Pm = game_object.GetComponent<player_movement>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        animator.SetBool("Touched", true);
        AP.GetComponent<AudioPlayer>().OpenChest();
        item();

    }

    void item() //when chest is opened, a random item is awarded to the player
    {

        if (!open)
        {
            objNum = Random.Range(0, 3);

            if (objNum == 0)
            {
                jump.gameObject.SetActive(true);
                Pm.JumpBoost(); //jump boost
            }
            if (objNum == 1)
            {
                gravity.gameObject.SetActive(true);
                Pm.GravitySwitch(); //player can change gravity
            }
            if (objNum == 2)
            {
                jet.gameObject.SetActive(true);
                Pm.Release(); //jet pack
            }
            open = true;
        }

    }
}
