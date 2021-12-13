using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoCam : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    private float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        cam1.gameObject.SetActive (true);
        cam2.gameObject.SetActive (false);
    }

    // Update is called once per frame

    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed; //keep camera moving right

    }

    public void switchCamera()
    {
            cam1.gameObject.SetActive (false);
            cam2.gameObject.SetActive (true);
    }

    public void Clamp()
    {
        cam1.GetComponent<autoCam>().enabled = false;
    }

    public void unClamp()
    {
        transform.position = new Vector3(-3.34f, 0f, -100f);
        cam1.GetComponent<autoCam>().enabled = true;
    }

}
