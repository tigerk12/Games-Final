using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision) //change to completed scene
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Completed");
        }
    }
}
