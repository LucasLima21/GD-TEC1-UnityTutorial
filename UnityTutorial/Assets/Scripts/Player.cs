using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed, JumpForce;
    public bool isJumping, doubleJump, isBlowing;
    private Rigidbody2D rig;
    private Animator anime;
    void Start()
    { 
        rig = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move(){
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        
        //move personagem em uma posição
        //transform.position += (movement * Time.deltaTime * Speed);
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        if(movement > 0f){
            anime.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        if(movement < 0f){
            anime.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        if(movement == 0){
            anime.SetBool("walk", false);
        }
    }

    void Jump(){
        if (Input.GetButtonDown("Jump") && !isBlowing){
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anime.SetBool("jump", true);
            }
            else{
                if(doubleJump){
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = false;
            anime.SetBool("jump", false);
        }
        if(collision.gameObject.tag == "Spike"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Saw"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.layer == 11){
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.layer == 11){
            isBlowing = false;
        }
    }
}
