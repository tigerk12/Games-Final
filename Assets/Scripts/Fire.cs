using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject launcher;
    public GameObject button;
    private dropBomb DB;

    bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        DB = launcher.GetComponent<dropBomb>();
    }

    void OnTriggerEnter2D(Collider2D collision) //shoot bomb at boss when button is pressed
    {
        if (collision.gameObject.tag == "Player" && !shot)
        {
            shot = true;
            DB.shoot();
            DB.enabled = false;
        }

    }
}
