using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { Start, PlayerTurn, AITurn, Won, Lost }

public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject AI;

    public Color colour;
    public Button[] buttons;
    public GameObject shield;
    public GameObject powerUp;
    public GameObject powerUp2;
    public Animator animator;
    public GameObject BG;
    public GameObject text;
    public GameObject home;
    public GameObject door;

    public AudioPlayer AP;

    bool healed = false;
    bool healable = true;
    int count = 0;

    Unit playerUnit;
    Unit AIUnit;

    public BattleState state;

    void Start() //set state to start
    {
        state = BattleState.Start;
        StartCoroutine(setUp());
    }

    void Update()
    {
        if (powerUp2.activeSelf) //if powerUp is max, turn off power up button 
        {
            buttons[2].enabled = false;
            buttons[2].GetComponent<Image>().color = colour;
        }
    }


    IEnumerator setUp() //set up HUD for player and AI
    {
        buttons[0].enabled = false;
        buttons[1].enabled = false;
        buttons[2].enabled = false;

        playerUnit = player.GetComponent<Unit>();
        AIUnit = AI.GetComponent<Unit>();

        playerUnit.SetHUD();
        AIUnit.SetHUD();


        yield return new WaitForSeconds(1f);


        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        AP.GetComponent<AudioPlayer>().Explosion();
        bool isDead = AIUnit.TakeDamage(playerUnit.damage);
        AIUnit.SetHP(AIUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.AITurn;
            AIEvaluate();
        }

    }

    IEnumerator Heal()
    {
        //heal 20hp (2 turn lock)
        AP.GetComponent<AudioPlayer>().IncHeart();
        playerUnit.currentHP += 20;
        playerUnit.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(2f);

        healed = true;

        state = BattleState.AITurn;
        AIEvaluate();
    }

    IEnumerator Power() //power up by 10 damage
    {
        AP.GetComponent<AudioPlayer>().PowerUp();

        if (powerUp.activeSelf)
        {
            powerUp.SetActive(false);
            powerUp2.SetActive(true);
            playerUnit.damage = 30;
        }
        else
        {
            powerUp.SetActive(true);
            playerUnit.damage = 20;
        }

        yield return new WaitForSeconds(2f);

        state = BattleState.AITurn;
        AIEvaluate();
    }

    //AI decides between attacking or lowering player's power
    int AttackMinMax()
    {
        if (playerUnit.currentHP == 50 || playerUnit.damage == 10)
            return 100;
        else if (playerUnit.damage <= 20)
            return 10;
        else if (playerUnit.damage == 30 && AIUnit.currentHP < 70)
            return -10;
        else 
            return 1;
    }

    int PowerMinMax()
    {
        if (playerUnit.currentHP > 40)
            return 99;
        else if (AIUnit.currentHP < 60 || playerUnit.damage > AIUnit.currentHP * 2)
            return 11;
        else 
            return -9;
    }

    //AI evaluates the different options they have at that point in tie
    void AIEvaluate()
    {

        if (playerUnit.currentHP <= AIUnit.damage)
            StartCoroutine(EnemyAttack());

        else if (healable && AIUnit.currentHP < 50 && AIUnit.currentHP <= playerUnit.damage * 2)
            StartCoroutine(EnemyHeal());

        else if (playerUnit.damage > 10)
        {
            int attackMove = AttackMinMax();
            int powerMove = PowerMinMax();

            if (attackMove > powerMove)
            {
                StartCoroutine(EnemyAttack());
            }
            else
            {
                StartCoroutine(PowerDown());
            }
        }
        else
        {
            StartCoroutine(EnemyAttack());
        }
    }


    IEnumerator EnemyAttack()
    {
        animator.SetTrigger("Attack");
        AP.GetComponent<AudioPlayer>().SwordSlash();
        bool isDead = playerUnit.TakeDamage(AIUnit.damage);
        playerUnit.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    IEnumerator EnemyHeal()
    {
        animator.SetTrigger("Heal");
        AP.GetComponent<AudioPlayer>().AIHeal();

        AIUnit.currentHP += 40;
        AIUnit.SetHP(AIUnit.currentHP);
        yield return new WaitForSeconds(1f);

        healable = false;

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    IEnumerator PowerDown()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("PowerDown");
        yield return new WaitForSeconds(1f);
        AP.GetComponent<AudioPlayer>().PowerDown();

        if (powerUp2.activeSelf)
        {
            powerUp.SetActive(true);
            powerUp2.SetActive(false);
            playerUnit.damage = 20;
        }
        else
        {
            powerUp.SetActive(false);
            playerUnit.damage = 10;
        }

        yield return new WaitForSeconds(1f);
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }


    void PlayerTurn()
    {
        if (healed)
        {
            count += 1;
            if (count == 3)
            {
                count = 0;
                healed = false;
            }
        }

        buttons[0].enabled = true;
        buttons[0].GetComponent<Image>().color = Color.white;
        if (count == 0)
        {
            buttons[1].enabled = true;
            buttons[1].GetComponent<Image>().color = Color.white;
        }
        buttons[2].enabled = true;
        buttons[2].GetComponent<Image>().color = Color.white;
        shield.SetActive(false);


    }

    void EndBattle() //Battle ends when either player loses or wins
    {
        if (state == BattleState.Won)
        {
            animator.SetTrigger("isDead");
            player.GetComponent<player_movement>().enabled = true;
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);

            door.SetActive(true);
        }
        else if (state == BattleState.Lost)
        {
            AP.GetComponent<AudioPlayer>().Death();
            player.GetComponent<Renderer>().material.color = Color.black;
            BG.GetComponent<Renderer>().material.color = Color.black;
            text.gameObject.SetActive(true);
            home.gameObject.SetActive(true);
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        OffButton();
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        OffButton();
        StartCoroutine(Heal());
    }

    public void PowerUpButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        OffButton();
        StartCoroutine(Power());
    }

    public void OffButton()
    {
        buttons[0].enabled = false;
        buttons[0].GetComponent<Image>().color = colour;
        buttons[1].enabled = false;
        buttons[1].GetComponent<Image>().color = colour;
        buttons[2].enabled = false;
        buttons[2].GetComponent<Image>().color = colour;
    }

}

