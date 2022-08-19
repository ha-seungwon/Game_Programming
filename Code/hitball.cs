using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitball : MonoBehaviour
{

    public float xpos;
    public float ypos;
    Rigidbody2D rigid;
    private float maxtime= 2.5f;
    private float ctime;
  
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        xpos = -6.948722f;
        ypos = 4.274f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.position.y < 0.39f)
        {

            rigid.MovePosition(new Vector2(xpos,ypos));
            rigid.velocity = new Vector2(0, 0);

        }
        
        Invoke("shoot",2);
        delay();
    }
    void shoot()
    {

        if (ctime < maxtime)
        {
            return; }

        rigid.velocity = new Vector2(8, rigid.velocity.y);
        
        ctime = 0;
    }
    void delay()
    {
        ctime += Time.deltaTime;

    }
    
}
