using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{

    public GameObject[] closedGate;
    public GameObject[] openGate;
    public GameObject[] indicator;
    public GameObject button;
    public GameObject player;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject BG;
    public GameObject BG2;


    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        indicator[0].gameObject.SetActive(false);
        indicator[1].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!open && !MovePlatform.move) // open first gate
            {
                open = true;
                player.GetComponent<player_movement>().enabled = false;
                BG.gameObject.SetActive(true);
                BG2.gameObject.SetActive(true);
                cam1.gameObject.SetActive(false);
                cam2.gameObject.SetActive(true);
                StartCoroutine(MoveCam());

            }
            else if (!open && MovePlatform.move) // next set of gates
            {
                MovePlatform2.move2 = true;
                button.gameObject.SetActive(true);

                closedGate[0].gameObject.SetActive(false);
                openGate[0].gameObject.SetActive(true);
                closedGate[1].gameObject.SetActive(false);
                openGate[1].gameObject.SetActive(true);
                indicator[0].gameObject.SetActive(true);
                indicator[1].gameObject.SetActive(true);

                EnemyAI.openGate = true;
            }
        } 
    }

    IEnumerator MoveCam() //move camera to specified point
    {
        MovePlatform.move = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(Open());
    }

    IEnumerator Open() //change camera as the first set of floors move and the first gate opens
    {
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        button.gameObject.SetActive(true);

        closedGate[0].gameObject.SetActive(false);
        openGate[0].gameObject.SetActive(true);

        EnemyAI.openGate = true;

        yield return new WaitForSeconds(2);
        BG.gameObject.SetActive(false);
        BG2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(false);
        cam1.gameObject.SetActive(true);
        indicator[0].gameObject.SetActive(true);
        player.GetComponent<player_movement>().enabled = true;
    }
}
