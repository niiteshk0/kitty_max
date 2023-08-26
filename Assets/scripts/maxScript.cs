    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxScript : MonoBehaviour
{
    public float jumpForce;
    public float speed = 2f;
    private float translation;
    private Animator anim;

    public LayerMask WhatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;

    private kittyScript kitty;
    private bool allowJump = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        kitty = GameObject.FindGameObjectWithTag("Player").GetComponent<kittyScript>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        translation = 1;
    }

    // Update is called once per frame

    void Update()
    {
        LeftRightDecision();

    }
    void FixedUpdate()
    {
        if (kitty.isDead)
        {
            translation = 0;
            anim.SetFloat("speed", 0);
            if (Grounded)
                rb.mass = 1000;
            return;
        }
        Debug.Log("kitty is dead");
        turnPlayer();

        AI();  

        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);

        if (translation > 0 || translation < 0)  //means 0 speed hua to ek jagha khada rhega or speed >0 hua to aage ki or run krega
        {                                      // or agr speed < 0 hua to peehe ki or run krega 0 speed chodh ke run dono case me krega ek ki speed se run krega
            anim.SetFloat("speed", 1);
        }

        if (translation == 0)
            anim.SetFloat("speed", 0);
    }

    void turnPlayer()
    {
        if (translation < 0)  //left move
            this.GetComponent<SpriteRenderer>().flipX = true;

        else if (translation > 0)  // right move
            this.GetComponent<SpriteRenderer>().flipX = false;
    }

    bool isGrounded()
    {
        if (Physics2D.OverlapCircle(groundPosition.position, 0.3f, WhatIsGround))
            return true;
        
        return false;
    }

    void jump()
    {
        if (!isGrounded())  //jb hmara character ground pe ni hoga to kuch ni krnge sirf return ho jyange
            return;

        Vector3 vec = rb.velocity;
        vec.y = 0;
        rb.velocity = vec;

        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "jumpCollid")
        {
            allowJump = true;
            LeftRightDecision();
        }
            
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "jumpCollid")
        {
            allowJump = false;
        }

    }
    

    public void SwitchDirection()
    {
        if (translation == 1)
            translation = -1;
        else
            translation = 1;
    }

    void LeftRightDecision()
    {
        // kitty ki x pos max ki x pos se jada h to max right move krega  (means kitty right me h to max ko bhi right jana h)
        if (kitty.getXPos-0.5f > transform.position.x)
        {
            translation = 1f;
        }
        else if (kitty.getXPos+0.5f < transform.position.x)    //kitty ki x pos max ki x pos se kam h to max bhi left move krega  (means kitty left me h to max ko bhi left jana h)
        {
            translation = -1f;
        }
    }

    void AI()      //jump krne ka AI
    {
        //jump decision
        // if player y position is greater than enemy y postiton then jump  and if enemy touch the jumpCollid
        if (kitty.getYPos > transform.position.y && allowJump)
            jump();
    }
}
