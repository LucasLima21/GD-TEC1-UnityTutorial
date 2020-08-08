using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed, moveTime;
    private bool directionRight = true;
    private float timer;
    void Start(){

    }
    void Update()
    {
        if(directionRight){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }else{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if(timer >= moveTime){
            directionRight = !directionRight;
            timer = 0f;
        }
    }
}
