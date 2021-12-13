using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class kill : MonoBehaviour
{
    [SerializeField]
    private TMP_Text showText;

    private Rigidbody2D rb;
    public Animator animator;
    public GameObject game_object;
    public GameObject camera;
    public GameObject[] Hearts;
    public GameObject BG1;
    public GameObject BG2;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject spawnPoint;
    public GameObject spawnPoint2;
    public GameObject canvas;
    public Color colour = Color.red;
    public AudioPlayer AP;

    private player_movement Pm;
    public static int maxLife = 3;
    private int currentLife;
    public static bool bossFight = false;
    bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        if(currentLife < 4)
            currentLife = 3;
        Pm = game_object.GetComponent<player_movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        showText.gameObject.SetActive(false);
        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Hearts[3].gameObject.SetActive(false);
        Hearts[4].gameObject.SetActive(false);

        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxLife > 3) //extra life
        {
            Hearts[3].gameObject.SetActive(true);
        }
        if (maxLife == 5)
        {
            Hearts[4].gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && UnlockLevel.level < 2 || collision.gameObject.tag == "Enemy" && UnlockLevel.level == 3)
        {
            dead();
        }
        else if (collision.gameObject.tag == "Enemy" && UnlockLevel.level == 2)
        {
            bossDead();
        }
        else if (collision.gameObject.tag == "Flock") //when killed by the flock, instantly lose all lives
        {
            FlockDeath();
        }

    }

    void dead () //death when level < 3
    {
        if (!isDead && currentLife > 1) //when killed, player movement is disabled and player is respawned 3 seconds later
        {
            isDead = true;
            currentLife = currentLife - 1;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            AP.GetComponent<AudioPlayer>().Death();
            animator.SetBool("isJumping", false);
            animator.SetBool("isDead", true);
            transform.Rotate(0.0f, 0f, 90f);
            Pm.enabled = false;
            rb.gravityScale = -1f;

            camera.GetComponent<CameraController>().Clamp();

            Destroy(Hearts[currentLife].gameObject);

            StartCoroutine(Respawn());

        }
        else if(!isDead && currentLife == 1)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            AP.GetComponent<AudioPlayer>().GameOver();
            animator.SetBool("isJumping", false);
            animator.SetBool("isDead", true);
            transform.Rotate(0.0f, 0f, 90f);
            Pm.enabled = false;
            rb.gravityScale = -1f;

            showText.gameObject.SetActive(true);
            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);

            camera.GetComponent<CameraController>().Clamp();


            BG1.GetComponent<Renderer>().material.color = colour;
            BG2.GetComponent<Renderer>().material.color = colour;

            Destroy(Hearts[currentLife].gameObject);

            Refresh();

        }

    }

    void bossDead() //death on level 3
    {
        if (!isDead && currentLife > 1)
        {
            isDead = true;
            currentLife = currentLife - 1;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            AP.GetComponent<AudioPlayer>().Death();
            animator.SetBool("isJumping", false);
            animator.SetBool("isDead", true);
            transform.Rotate(0.0f, 0f, 90f);
            Pm.enabled = false;
            rb.gravityScale = -1f;

            camera.GetComponent<autoCam>().Clamp();

            Destroy(Hearts[currentLife].gameObject);

            StartCoroutine(bossRespawn());
        }

        else if (!isDead && currentLife == 1)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            AP.GetComponent<AudioPlayer>().GameOver();
            animator.SetBool("isJumping", false);
            animator.SetBool("isDead", true);
            transform.Rotate(0.0f, 0f, 90f);
            Pm.enabled = false;
            rb.gravityScale = -1f;

            showText.gameObject.SetActive(true);
            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);

            camera.GetComponent<autoCam>().Clamp();

            Destroy(Hearts[currentLife].gameObject);

            Refresh();

        }

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        this.transform.position = spawnPoint.transform.position;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        camera.GetComponent<CameraController>().unClamp();

        animator.SetBool("isDead", false);
        transform.Rotate(0.0f, 0f, -90f);
        Pm.enabled = true;
        rb.gravityScale = 9.8f;
        isDead = false;
    }

    IEnumerator bossRespawn()
    {
        if (!bossFight)
        {
            yield return new WaitForSeconds(3);
            this.transform.position = spawnPoint.transform.position;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            camera.GetComponent<autoCam>().unClamp();

            animator.SetBool("isDead", false);
            transform.Rotate(0.0f, 0f, -90f);
            Pm.enabled = true;
            rb.gravityScale = 9.8f;
            isDead = false;
        }

        if (bossFight) // change spawnpoint to boss area
        {
            yield return new WaitForSeconds(3);
            this.transform.position = spawnPoint2.transform.position;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            camera.GetComponent<autoCam>().unClamp();

            animator.SetBool("isDead", false);
            transform.Rotate(0.0f, 0f, -90f);
            Pm.enabled = true;
            rb.gravityScale = 9.8f;
            isDead = false;

            Refresh();
        }
    }

    void Refresh()
    {
        currentLife = 3;
    }

    public void Transport()
    {
        this.transform.position = spawnPoint2.transform.position;
    }

    void FlockDeath()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        AP.GetComponent<AudioPlayer>().Death();
        animator.SetBool("isJumping", false);
        animator.SetBool("isDead", true);
        transform.Rotate(0.0f, 0f, 90f);
        Pm.enabled = false;
        rb.gravityScale = -1f;

        showText.gameObject.SetActive(true);
        Button1.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);

        BG1.GetComponent<Renderer>().material.color = colour;
        BG2.GetComponent<Renderer>().material.color = colour;

        Refresh();
    }

}
