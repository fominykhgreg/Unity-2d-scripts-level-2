using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    public AudioSource myFx;
    public AudioClip DamageFX;
    public AudioClip DeathFX;

    public int maxHealth = 3;
    int currentHealth;

    public void SoundDamage()
    {
        myFx.PlayOneShot(DamageFX);
    }

    public void SoundDeath()
    {
        myFx.PlayOneShot(DeathFX);
    }





    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {

        SoundDamage();
        currentHealth -= damage;
        animator.SetTrigger("dmg");

        if (currentHealth <= 0)
        {
            Die();
            SoundDeath();
        }
    }
    void Die()
    {

        animator.SetBool("isDead", true);
        SoundDeath();
       
        GetComponent < Collider2D>().enabled = false;
        this.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
