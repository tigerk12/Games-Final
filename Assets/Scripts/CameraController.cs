using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;
    public GameObject cam2;
    private Transform target;
    private Vector3 offset;

    public float smoothSpeed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); //move camera with player
    }

    public void Clamp()
    {
        camera.GetComponent<CameraController>().enabled = false;
    }

    public void unClamp()
    {
        camera.GetComponent<CameraController>().enabled = true;
    }

    public void ChangeCam()
    {
        camera.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true); 
    }
}
