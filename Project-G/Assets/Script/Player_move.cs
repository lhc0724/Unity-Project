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

    Map_Triggers mapEvt;
    DialogManager dialogEvt;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D> ();

        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();

        mapEvt = GameObject.Find("Map").GetComponent<Map_Triggers>();
        dialogEvt = GameObject.Find("Canvas_Textbox").GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !anim.GetBool("jump")) {
            anim.SetTrigger("do_jump");
            Jump();
        }

        Move();

        if(rigid.position.y <= -8.0f) {
            mapEvt.init_obj_position("Player");
        }
    }

    void FixedUpdate()
    {
    }

    void Move() {
        Vector3 moveVelocity = Vector3.zero;
        float f_direction = Input.GetAxisRaw("Horizontal");

        //Debug.Log(f_direction);
        
        if(f_direction != 0) {
            moveVelocity = new Vector3(f_direction, 0, 0);

            if(f_direction > 0) {
                renderer.flipX = false;
            }else {
                renderer.flipX = true;
            }

            anim.SetInteger("move", 1);
            transform.position += moveVelocity * movePower * Time.deltaTime;
        } else {
            anim.SetInteger("move", 0);
        }
    }

    void Jump()
    {
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Attach : " + other.gameObject.layer); 

        switch (other.gameObject.layer) {
            //case 0:
            case 3:
                anim.SetBool("jump", false);
                break;

            default:
                break;
        }

        if(other.gameObject.tag == "Stage_End") {
            mapEvt.StageEnd();
        }

        if(other.gameObject.tag == "Start") {
            dialogEvt.ReqViewDialog();
            Destroy(GameObject.Find("Map").transform.Find("PointStart").gameObject);
        }

        if(other.gameObject.name == "Trigger_1") {
            dialogEvt.ReqViewDialog();
            Destroy(GameObject.Find("Map").transform.Find("Trigger_1").gameObject);
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
