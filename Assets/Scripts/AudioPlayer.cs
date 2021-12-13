using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    public AudioSource BGM;
    public AudioSource pickUpTune;
    public AudioSource incHeart;
    public AudioSource chest;
    public AudioSource jbTune;
    public AudioSource level3BGM;
    public AudioSource bossBGM;
    public AudioSource death;
    public AudioSource gameOver;
    public AudioSource aiHeal;
    public AudioSource sword;
    public AudioSource explosinon;
    public AudioSource powerUp;
    public AudioSource powerDown;


    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StarTune()
    {
        pickUpTune.Play();
    }    
    
    public void IncHeart()
    {
        incHeart.Play();
    }
    
    public void OpenChest()
    {
        chest.Play();
    }    
    
    public void JumpBoostTune()
    {
        jbTune.Play();
    } 
    
    public void BossFight()
    {
        level3BGM.Pause();
        bossBGM.Play();
    }
    
    public void Death()
    {
        death.Play();
    }
    
    public void GameOver()
    {
        gameOver.Play();
    }
    
    public void AIHeal()
    {
        aiHeal.Play();
    }
    
    public void SwordSlash()
    {
        sword.Play();
    }
    
    public void Explosion()
    {
        explosinon.Play();
    } 
    
    public void PowerUp()
    {
        powerUp.Play();
    } 
    
    public void PowerDown()
    {
        powerDown.Play();
    } 
    
}
