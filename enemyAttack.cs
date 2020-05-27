using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    GameObject player;
    public Animator animator;
    public float speedMove = 1.3f;

    public AudioSource myFx;
    public AudioClip HitFX;
    public AudioClip PrivetFX;

    public void SoundHit()
    {
        myFx.PlayOneShot(HitFX);
    }
    public void SoundPrivet()
    {
        myFx.PlayOneShot(PrivetFX);
    }


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    public Transform AttackPoint;
    
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage;
    public float attackDelay;
    public float attackSoundDelay;


    bool attack = true;
    bool attackAnim = true;

    public void moshnost()
    {
        lifeScriptAlf.lifeValue -= attackDamage;
    }
    void AttackReset()
    {
        attack = true;
    }
    void AttackResetAnim()
    {
        attackAnim = true;
    }

    void Attack()
    {
        if (attack == true)
        {
            attack = false;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
           
            //dmg
            foreach (Collider2D enemy in hitEnemies)
            {
                //Debug.Log("Hit" + enemy.name);
                //enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
                // lifeScript.lifeValue -= 1;
                FindObjectOfType<AlfredWarrior>().damaga();
                moshnost();
            }
            
            Invoke("AttackReset", 3);
        }

    }


    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
       
    }
    


    void Update()
    {
        float direction = player.transform.position.x - transform.position.x;
        float directionVert = player.transform.position.y - transform.position.y;

        if (direction < -0.2)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Mathf.Abs(direction) < 7 && Mathf.Abs(directionVert) < 5 && Mathf.Abs(direction) > 2)
        {
            
            Vector3 pos = transform.position;
            pos.x += Mathf.Sign(direction) * speedMove * Time.deltaTime;
            transform.position = pos;
            animator.SetInteger("walk", 0);
        }
        if (Mathf.Abs(direction) < 2)
        {
            for (int i = 0; i < 100; i++) {
                if (attackAnim == true)
                {
                    attackAnim = false;
                    animator.SetTrigger("atk");
                    Invoke("AttackResetAnim", 3);
                    Invoke("SoundHit", attackSoundDelay);
                }

                Invoke("Attack", attackDelay);
                

            }
        }
        if (Mathf.Abs(direction) > 6 && Mathf.Abs(direction) < 6.1 && !myFx.isPlaying)
        {
            SoundPrivet();
         }
    }
    void AI()
    {

    }
}

