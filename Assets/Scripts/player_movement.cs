using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject Jetpack;
    public GameObject camera;
    public GameObject player;
    public AudioPlayer AP;
    public FlockSpawn FS;

    public float jumpForce = 20f;
    private float runSpeed = 20f;
    private float horizontalMove = 0f;
    private float verticalMove;
    private float jetForce = 30f;
    private float jetDirection = 2f;
    private Renderer rend;
    private SpriteRenderer spriteRenderer;

    bool jump;
    bool Boosted;
    bool Grounded = true;
    bool Allowed = false;
    bool Ready = true;
    bool Jet = false;
    bool open;

    [SerializeField]
    private Color colour = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        jump = false;
        rend = GetComponent<Renderer>();
        Jetpack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * jumpForce;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        //flip player
        if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
        }
       

        animator.SetFloat("yVelocity", rb.velocity.y);

        if (Allowed) //change gravity
        {
            if (Input.GetKeyDown(KeyCode.Space) && Grounded && Ready)
            {
                StartCoroutine(Up());
            }

            if (Input.GetKeyDown(KeyCode.Space) && !Grounded && !Ready)
            {
                StartCoroutine(Down());
            }
        }

    }

    void FixedUpdate()
    {
        if (horizontalMove > 0.1f || horizontalMove < -0.1f)
        {
            rb.AddForce(new Vector2(horizontalMove, 0f), ForceMode2D.Impulse);
        }

        if (!jump && verticalMove > 0.1f)
        {
            rb.AddForce(new Vector2(0f, verticalMove*jumpForce), ForceMode2D.Impulse);

        }

        if (Jet) //while pressing space jetpack can be used
        {
            if (Input.GetButton("Space"))
            {
                rb.AddForce(new Vector2(0f, jetDirection * jetForce), ForceMode2D.Impulse);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform" && !Boosted)
        {
            animator.SetBool("isJumping", false);
            jump = false;
            open = false;
        }

        if (collision.gameObject.tag == "Platform" && Boosted)
        {
            animator.SetBool("JumpBoost", false);
            jump = false;
        }

        if (collision.gameObject.tag == "Door" && !open)
        {
            open = true;
            UnlockLevel.level += 1;
            showStar.level += 1;
        }

        if (collision.gameObject.tag == "Finish")
        {
            if(showStar.level == 2)
            {
                kill.bossFight = true;
                camera.GetComponent<autoCam>().switchCamera();
                AP.GetComponent<AudioPlayer>().BossFight();
            }
            else if (showStar.level == 3)
            {
                camera.GetComponent<CameraController>().ChangeCam();
                player.GetComponent<kill>().Transport();
                FS.GetComponent<FlockSpawn>().Spawn();
            }

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && !Boosted)
        {
            animator.SetBool("isJumping", true);
            jump = true;
        }

        if (collision.gameObject.tag == "Platform" && Boosted)
        {
            animator.SetBool("JumpBoost", true);
            jump = true;
            AP.GetComponent<AudioPlayer>().JumpBoostTune();
        }

    }

    public void OnLadning ()
    {
        animator.SetBool("isJumping", false);
    }

    public void Holding () //while object is held jumpforce is lower
    {
        jumpForce = 15f;
    }

    public void Dropped()
    {
        if (Boosted) //jump boost activated
        {
            jumpForce = 30f;
            Boosted = true;
            rb.gravityScale = 8f;
        }
        else
        {
            jumpForce = 20f;
            rb.gravityScale = 9.8f;
        }
    }

    public void Gravity() // when heavy block is held graity increases to 22
    {
        rb.gravityScale = 22f;
    }

    public void JumpBoost()
    {
        jumpForce = 30f;
        Boosted = true;
        rb.gravityScale = 8f;
    }

    public void GravitySwitch()
    {
        rend.material.color = colour;
        Allowed = true;
    }

    IEnumerator Up()
    {
        Ready = false;
        rb.gravityScale = -10f;
        transform.Rotate(0.0f, 180f, 180f);
        AP.GetComponent<AudioPlayer>().JumpBoostTune();
        yield return new WaitForSeconds(1);
        Grounded = false;
    }

    IEnumerator Down()
    {
        Ready = true;
        rb.gravityScale = 9.8f;
        transform.Rotate(0.0f, 180f, 180f);
        AP.GetComponent<AudioPlayer>().JumpBoostTune();
        yield return new WaitForSeconds(1);
        Grounded = true;
    }

    public void Release()
    {
        Jetpack.SetActive(true);
        Jet = true;
    }

}