using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{   
    private Rigidbody2D rigi;
    private Animator anime;
    public float speed;
    
    public Transform rightCollumn, leftCollumn, headPoint;
    private bool colliding, playerDestroyed;
    public LayerMask layer;
    // Start is called before the first frame update
    public BoxCollider2D box;
    public CircleCollider2D circle;
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // o segundo parametro do vector 2 é o proprio eixo y para q ele nao se moviemnte no eixo y
        rigi.velocity = new Vector2(speed, rigi.velocity.y);
        colliding = Physics2D.Linecast(rightCollumn.position, leftCollumn.position, layer);
        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            float height = collision.contacts[0].point.y - headPoint.position.y;
            
            if(height > 0 && !playerDestroyed){
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.up * 10), ForceMode2D.Impulse);
                speed = 0;
                anime.SetTrigger("die");
                box.enabled = false;
                circle.enabled = false;
                rigi.bodyType = RigidbodyType2D.Kinematic;
                //destroy inimigo
                Destroy(gameObject, 0.33f); 
            }
            else{
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                //destroy player
                Destroy(collision.gameObject);
            }
        }
    }
}
