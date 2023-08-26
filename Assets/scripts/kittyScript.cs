using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kittyScript : MonoBehaviour
{
    public float jumpForce;
    public float speed = 2f;
    private float translation;
    private Animator anim;
    public GameObject max;

    public Text ScoreText;

    public GameObject particle;

    public LayerMask WhatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;
    public bool isDead = false;


    private Rigidbody2D rb;

    public float getXPos    //ye hmme kitty ki x position btayega
    {
        get
        {
            return transform.position.x;
        }
    }

    public float getYPos     
    {
        get
        {
            return transform.position.y;
        }
    }

    public bool isMovingRight
    {
        get
        {
            if (translation == 1)
                return true;

            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            jump();
    }
    void FixedUpdate()
    {
        turnPlayer();

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            translation = Input.GetAxisRaw("Horizontal");   //input for keyboard
        }
        //transform.Translate(new Vector3(translation, 0f, 0f)*Time.deltaTime*speed);
        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);
        
        if(translation > 0 || translation < 0)  //means 0 speed hua to ek jagha khada rhega or speed >0 hua to aage ki or run krega
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

    public void jump()
    {
        if (!isGrounded())  //jb hmara character ground pe ni hoga to kuch ni krnge sirf return ho jyange
            return;

        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "coin")
        {
            //update the coin text
            ScoreText.text = (int.Parse(ScoreText.text) + 5).ToString();

            GameObject p = Instantiate(particle, coll.transform.position, particle.transform.rotation);
            Destroy(p, 0.5f);
            Destroy(coll.gameObject);
        }

        if(coll.tag == "home")
        {
            if(transform.childCount > 1)
            {
                GameObject chicken = transform.GetChild(1).gameObject;
                chicken.GetComponent<Chicken>().follow = false;
                chicken.transform.parent = null;
                chicken.GetComponent<Collider2D>().enabled = false;
                chicken.GetComponent<chickenRun>().enabled = true;

                StartCoroutine(ChickenDestroy(chicken));
            }
        }

    }
    IEnumerator ChickenDestroy(GameObject chicken)
    {
        yield return new WaitForSeconds(1f);
        chicken.SetActive(false);
        Destroy(chicken);

        int chickenCount = GameObject.FindGameObjectsWithTag("Chicken").Length;

        if(chickenCount == 0)
        {
            UIHandler.instance.ShowLevelDailog("Level Complete", ScoreText.text);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Max")
        {
            if(!isDead)
            {
                isDead = true;
                anim.SetTrigger("death");
                UIHandler.instance.ShowLevelDailog("Level Failed", ScoreText.text);
            }
        }
    }

    public void OnPointerEnter_right()   //right button touch hote he right jaye
    {
        translation = 1;    
    }

    public void OnPointerExit()     // right ya left se touch hat te he stop ho jaye
    {
        translation = 0;
    }
    public void OnPointerEnter_Left()  // left pe touch hote he left jaye 
    {
        translation = -1;
    }
}
