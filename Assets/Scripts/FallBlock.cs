using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallBlock : MonoBehaviour //purple blocks on level 2
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D collision) //when player stands on block, rigidbody is turned off and block falls down
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Drop());
        }
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(1);
        rb.isKinematic = false;


    }


}
