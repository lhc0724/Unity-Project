using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer_move : MonoBehaviour
{
    public float movePower = 4.0f;
    public float jumpPower = 3.0f;

    Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator anim;

    Vector3 movement;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D> ();
        rigid.position = Vector2.zero;
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
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
        if(!isJumping) {
            return ;
        }

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
}
