using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    public float movePower = 4.0f;
    public float jumpPower = 5.5f;

    Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator anim;

    Vector3 movement;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D> ();
        //rigid.position = Vector2.zero;

        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !anim.GetBool("jump")) {
            anim.SetTrigger("do_jump");
            jump = true;
        }

        move();
        Jump();
    }

    void FixedUpdate()
    {
    }

    void move() {
        Vector3 moveVelocity = Vector3.zero;
        float f_movechk = Input.GetAxisRaw("Horizontal");

        if(f_movechk < 0)  {
            moveVelocity = Vector3.left;
            renderer.flipX = true; //left filp
        } else if (f_movechk > 0) {
            moveVelocity = Vector3.right;
            renderer.flipX = false; //right filp
        }

        if (f_movechk != 0) {
            anim.SetInteger("move", 1);
        }else {
            anim.SetInteger("move", 0);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if(!jump) {
            return ;
        }

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Attach : " + other.gameObject.layer); 

        switch (other.gameObject.layer) {
            case 3:
                anim.SetBool("jump", false);
                break;

            default:
                break;
        }

        if(other.gameObject.tag == "Stage_End") {
            Map_Triggers.StageEnd();
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Detach : " + other.gameObject.layer);
        switch (other.gameObject.layer) {
            case 3:
                anim.SetBool("jump", true);
                break;

            default:
                break;
        }
    }
}
