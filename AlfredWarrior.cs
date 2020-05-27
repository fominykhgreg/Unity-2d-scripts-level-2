using UnityEngine;
using System.Collections;

using System.Diagnostics;


public class AlfredWarrior : MonoBehaviour {

    [SerializeField] float m_speed = 1.0f;
    [SerializeField] float m_jumpForce = 2.0f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;


    bool isGrounded;


    //паралакс мой родной
    public static float parx;
    public static float parx2;

    public float parxSpeed;
    public float parxSpeed2;

    //movement
    public float horSpeed;
    float speedX;
    private static bool isFacingRight = true;


    //смерть
    public float deathDelay;

    void death()
    {
        //destroy();
        FindObjectOfType<gameManagerAlf>().gameOver();
    }



    //Атака уадр битва кия
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 1;
    public float attackDelay;
    public float attackSoundDelay;


    bool attack = true;
    bool attackAnim = true;


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
                enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
            }

            Invoke("AttackReset", 1);
        }

    }


    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }








    //звуки мои будут тут !
    public AudioSource myFx;
    public AudioClip walkFx;
    public AudioClip CherryFX;
    public AudioClip HitFX;
    public AudioClip ChestFX;
    public AudioClip StarFX;
    public AudioClip DamageFX;
    public AudioClip JumpFX;

    public void walk()
    {
        myFx.PlayOneShot(walkFx);
    }
    public void SoundCherry()
    {
        myFx.PlayOneShot(CherryFX);
    }
    public void SoundHit()
    {
        myFx.PlayOneShot(HitFX);
    }
    public void SoundChest()
    {
        myFx.PlayOneShot(ChestFX);
    }
    public void SoundStar()
    {
        myFx.PlayOneShot(StarFX);
    }

    public void SoundDamage()
    {
        myFx.PlayOneShot(DamageFX);
    }

    public void SoundJump()
    {
        myFx.PlayOneShot(JumpFX);
    }

    // Понеслась...

    void Start() {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }


    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    // Update is called once per frame




    void Update() {

        if (Time.timeScale == 0f)
        {
            return;
        }


        parx = 0;
        parx2 = 0;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!myFx.isPlaying && isGrounded)
            {
                walk();
            }

            speedX = -horSpeed;
            parx = -parxSpeed;
            parx2 = -parxSpeed2;




            if (speedX < 0 && isFacingRight)
            {
                Flip();

            }
        }



        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!myFx.isPlaying && isGrounded)
            {
                walk();
            }

            speedX = horSpeed;
            parx = parxSpeed;
            parx2 = parxSpeed2;



            if (speedX > 0 && !isFacingRight)
            {
                Flip();

            }
        }
        transform.Translate(speedX, 0, 0);
        speedX = 0;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // второстепенное управление!
        float inputX = Input.GetAxis("Horizontal");

        // смена направления(устарело)
        //if (inputX > 0)
        //    GetComponent<SpriteRenderer>().flipX = true;
        //else if (inputX < 0)
        //  GetComponent<SpriteRenderer>().flipX = false;

        // Move
        // m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);


        //Death
        if (Input.GetKeyDown("e")) {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
        if (lifeScriptAlf.lifeValue <= 0)
        {
            //      SoundDeath();
            // animator.SetBool("isDead", true);
            Invoke("death", deathDelay);

        }

        //дэмэдж
        //else if (Input.GetKeyDown("q"))


        //атака

        else if (Input.GetKeyDown("w"))
        {

            if (attackAnim == true)
            {
                attackAnim = false;
                m_animator.SetTrigger("Attack");
                Invoke("AttackResetAnim", 1);
                Invoke("SoundHit", attackSoundDelay);
            }

            Invoke("Attack", attackDelay);
        }


        //Смена позиции (не знаю еще для чгео)
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;



        //прыжок
        else if (Input.GetKeyDown("space") && m_grounded) {
            SoundJump();
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    void Dscript()
    {
        SoundDamage();
        m_body2d.velocity = Vector3.zero;
        m_body2d.AddForce(transform.up * 6.0F, ForceMode2D.Impulse);
        m_animator.SetTrigger("Hurt");
    }

    public void damaga()
    {
        SoundDamage();
        m_body2d.velocity = Vector3.zero;
        m_body2d.AddForce(transform.up * 6.0F, ForceMode2D.Impulse);
        //lifeScriptAlf.lifeValue -= 3;
        //FindObjectOfType<enemyAttack>().moshnost();
        m_animator.SetTrigger("Hurt");
    }


   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //SoundDamage();
            //m_body2d.velocity = Vector3.zero;
            //m_body2d.AddForce(transform.up * 6.0F, ForceMode2D.Impulse);
            // lifeScript.lifeValue -= 1;
            // m_animator.SetTrigger("Hurt");
            // SoundDamage();
            // m_body2d.velocity = Vector3.zero;
            //  m_body2d.AddForce(transform.up * 6.0F, ForceMode2D.Impulse);
            lifeScriptAlf.lifeValue -= 1;
            Dscript();
            // m_animator.SetTrigger("Hurt");
            // damaga();

        }
        if (collision.gameObject.tag == "cat")
        {
            Dscript();
            lifeScriptAlf.lifeValue -= 4;



        }
        if (collision.gameObject.tag == "burningman")
        {
            Dscript();
            lifeScriptAlf.lifeValue -= 3;

        }
        if (collision.gameObject.tag == "skeleton")
        {
            Dscript();
            lifeScriptAlf.lifeValue -= 2;

        }

        if (collision.gameObject.tag == "Cherry")
        {
            SoundCherry();
        }

        if (collision.gameObject.tag == "Chest")
        {
            SoundChest();
        }
        if (collision.gameObject.tag == "Star")
        {
            SoundStar();
        }

        

        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;

    }


     
}
