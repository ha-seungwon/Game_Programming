using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float maxspeed;
    public float jumppower;
    public float maxjump;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public bool isjumping;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {//buttonup the horizontal key to stop the move 
            //makes vector to 1 => normalized
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        if (Input.GetButtonUp("Jump") && rigid.velocity.y == 0)
        {
            anim.SetBool("isjump", true);

            Debug.Log("jp" + jumppower);
            rigid.velocity = new Vector2(rigid.velocity.x, jumppower);
            CancelInvoke("delay");
            jumppower = 0.0f;

        }

        //direction change
        if (Input.GetButton("Horizontal")) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //animation
        if (Mathf.Abs(rigid.velocity.x) <0.3)
        {
            anim.SetBool("isrwalking", false);
        }
        else
        {
            anim.SetBool("isrwalking", true);
        }
        if (Mathf.Abs(rigid.velocity.y) == 0) anim.SetBool("isjump", false);

       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            Debug.Log("hithit");
            OnDamaged(collision.transform.position);
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;

        //hit
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //reaction
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc * 7, 1) , ForceMode2D.Impulse);
        Invoke("OffDamaged", 3);//delay
        
    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");
        //move
        rigid.AddForce(Vector2.right * move, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxspeed) rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxspeed * (-1)) rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);

        if (Input.GetButtonDown("Jump"))

        {
            InvokeRepeating("delay", 0.5f, 0.5f);
            anim.SetBool("isjump", true);
            jumppower = delay();

        }

    }

    float delay()
    {
        Debug.Log("기모으는중....");
        Debug.Log("jump" + jumppower);

        if (Input.GetButtonUp("Jump"))return jumppower;
        else
        {
            
                if (jumppower >= maxjump)
            {
                Debug.Log("maxjump!!");
                return maxjump;
            }
            else {
                return jumppower += 2.0f;
            }
        }
    }


}
    