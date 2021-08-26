using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer_move : MonoBehaviour
{
    public float movePower = 1.0f;
    public float jumpPower = 1.0f;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D> ();
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        move();
        Jump();
    }

    void move() {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") < 0)  {
            moveVelocity = Vector3.left;
        } else if (Input.GetAxisRaw("Horizontal") > 0) {
            moveVelocity = Vector3.right;
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
