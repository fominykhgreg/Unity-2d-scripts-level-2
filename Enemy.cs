using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private Animator animator;
    public AudioSource myFx;
    public AudioClip DamageFX;
    public int health = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SoundDamage()
    {
        myFx.PlayOneShot(DamageFX);
    }


    public void TakeDamage(int damage)


    {

        animator.SetTrigger("dmg");
        SoundDamage();
    health -=damage;
        if(health <= 0)
        {
            SoundDamage();
            Die();
        }
    }

   void Die()
{
        
        Destroy(gameObject);
}

}
