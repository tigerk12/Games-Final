using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completed : MonoBehaviour
{
    public GameObject compText;
    public GameObject text;
    public GameObject[] buttons;


    // Start is called before the first frame update
    void Start()
    {
        if(showStar.level == 4)
        {
            compText.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }
    }

}
