using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    public static float parx;
    public static float parx2;


    public float horSpeed;
   
    public float parxSpeed;
    public float parxSpeed2;

    float speedX;
    private Animator animator;
    private SpriteRenderer sprite;
    Rigidbody2D rb;
    bool isGrounded;
    private float jumpForce = 6.6F;
    private Animator anim;
    private static bool isFacingRight = true;

    bool attack = true;
    public float attackdelay;

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







    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private CharState1 State
    {
        get { return (CharState1)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }





    void AttackReset()
    {
        attack = true;
    }

    public void Attack()
    {
        if (attack == true)
        {
            attack = false;
            FindObjectOfType<weapon>().Shoot();
           
            
            if (!myFx.isPlaying)
            {
                SoundHit();
            }
            Invoke("AttackReset", 2);
        }
    }













    // Update is called once per frame
    void Update()
    {
        parx = 0;
        parx2 = 0;
        State = CharState1.idle;
        animator.SetBool("ataka", false);


        if (Input.GetKeyDown(KeyCode.W))
        {
            Invoke("Attack", attackdelay);
            if (attack == true)
            {
                //State = CharState1.udar;
                animator.SetBool("ataka", true);
            }
        }

            if (Input.GetKey(KeyCode.LeftArrow))
        {
         
            speedX = -horSpeed;
            parx = -parxSpeed;
            parx2 = -parxSpeed2;

            if (!myFx.isPlaying && isGrounded)
            { //проигрываем новый звук, только если сейчас никакой звук не играет

                walk();

            }

            State = CharState1.run;
            animator.SetBool("ataka",false);
            if (speedX < 0 && isFacingRight)
            {
                Flip();

            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            speedX = horSpeed;
            parx = parxSpeed;
            parx2 = parxSpeed2;

            if (!myFx.isPlaying && isGrounded)
            { //проигрываем новый звук, только если сейчас никакой звук не играет

                walk();

            }
            State = CharState1.run;
            animator.SetBool("ataka", false);
            if (speedX > 0 && !isFacingRight)
            {
                Flip();

            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("juno", true);
            SoundJump();
            //State = CharState1.jump;
            // rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        transform.Translate(speedX, 0, 0);
        speedX = 0;

        if (!isGrounded)
        {
           // State = CharState1.jump;
            
        }

        if (!isGrounded)
        {
            // State = CharState1.jump;
            animator.SetBool("juno", false);
        }
        

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


    
    private void OnCollisionEnter2D(Collision2D collision)
    {

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
public enum CharState1
{
    idle,
    run,
    jump,
    udar
}