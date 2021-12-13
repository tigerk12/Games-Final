using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class open_door : MonoBehaviour
{

    [SerializeField]
    private Text openText;

    public string sceneName;
    private bool allowed;

    // Start is called before the first frame update
    private void Start()
    {
        openText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (allowed && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName); //press E to change scene
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openText.gameObject.SetActive(true);
            allowed = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openText.gameObject.SetActive(false);
            allowed = false;
        }
    }

}
