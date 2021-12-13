using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceDrop : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform") //float up when mace hits floor
        {
            StartCoroutine(Up());
        }

        if (collision.gameObject.tag == "Block") //drop gravity when mace hits ceiling
        {
            StartCoroutine(Down());

        }
        
    }

    IEnumerator Up()
    {
        yield return new WaitForSeconds(2);
        rb.gravityScale = -3f;
    }

    IEnumerator Down()
    {
        yield return new WaitForSeconds(1);
        rb.gravityScale = 20f;
    }


}
