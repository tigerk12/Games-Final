using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
        }
    }
}
