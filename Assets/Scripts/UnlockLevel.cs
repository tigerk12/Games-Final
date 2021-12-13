using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel : MonoBehaviour
{
    public GameObject[] contruction;
    public GameObject[] door;    


    public static int level;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() //remove construction off the doors when previous level is completed
    {
        
        if(level > 0 )
        {
            contruction[0].SetActive(false);
            door[0].GetComponent<Collider2D>().enabled = true;
        }

        if(level > 1) 
        {
            contruction[1].SetActive(false);
            door[1].GetComponent<Collider2D>().enabled = true;
        }
        
        if(level > 2) 
        {
            contruction[2].SetActive(false);
            door[2].GetComponent<Collider2D>().enabled = true;
        }
        
        if(level > 3) 
        {
            contruction[3].SetActive(false);
            door[3].GetComponent<Collider2D>().enabled = true;
        }
    }

}
