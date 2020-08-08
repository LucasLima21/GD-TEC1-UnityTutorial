using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float jumpForce;
    public bool isUp;
    public Animator anime;
    public int health = 5;
    public GameObject effect;
    void Update(){
        if(health <= 0){
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            if(isUp){
                anime.SetTrigger("hit");
                health--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else{
                anime.SetTrigger("hit");
                health--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }

        }
    }
}
