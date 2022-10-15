using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;

    private SpriteRenderer sr;

    void Awake(){
        sr=GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {  
        if(gameObject!=null){
            if(transform.position.y<-5){
                Destroy(gameObject);
                
            }
            RaycastHit2D hit=Physics2D.Raycast(transform.position,new Vector2(XMoveDirection,0));
            gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(XMoveDirection,0)*EnemySpeed;

            if(hit.collider!=null && hit.distance<1){
                if(hit.collider.tag=="Player"){
                    // Destroy(hit.collider.gameObject);
                    // FindObjectOfType<SoundManager>().Play("Die");
                    Debug.Log("Player Collided");
                }
                Flip ();
            }
        }
        
    }
    void Flip(){
        if(XMoveDirection>0){
            XMoveDirection=-1;
            sr.flipX=true;
        }
        else{
            XMoveDirection=1;
            sr.flipX=false;
        } 
        
    }
}
