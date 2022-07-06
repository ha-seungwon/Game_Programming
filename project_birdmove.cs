using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdmove : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rigid.velocity.y == 0)
        {
            anim.SetBool("isfly", true);


            rigid.velocity = new Vector2(rigid.velocity.x, 4);

        }
    }

}
