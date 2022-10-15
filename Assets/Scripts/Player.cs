using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveForce=10;
    [SerializeField]public float moveJump =7;
    private float movementX;
    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;
    private bool isGrounded =true;
    public GameObject poleFlag;



    // Start is called before the first frame update
    void Awake()
    {
        myBody=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Playermovement();
        Animateplayer();
        PlayerRaycast();
    }
    void FixedUpdate()
    {
        Playerjump();
    }
    void Playermovement(){
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX,0f,0f)*Time.deltaTime*moveForce;
    }
    void Animateplayer(){
        if(movementX>0){
            anim.SetBool("Walk",true);
            sr.flipX=false;
        }
        else if (movementX<0){
            anim.SetBool("Walk",true);
            sr.flipX=true;
        }
        else {
            anim.SetBool("Walk",false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("QuestionBlock")){
            isGrounded=true;
            anim.SetBool("Jump",false);
        } 
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Levelend")){
            Debug.Log("Level has ended");
            FindObjectOfType<SoundManager>().Play("LevelEnd");
            poleFlag.GetComponent<Rigidbody2D>().gravityScale=3;
            
        }
    }
    void Playerjump(){
        if(Input.GetButtonDown("Jump") && isGrounded){
            isGrounded=false;
            anim.SetBool("Jump",true);
            myBody.AddForce(new Vector2(0f,moveJump), ForceMode2D.Impulse);
            FindObjectOfType<SoundManager>().Play("Jump");
            FindObjectOfType<SoundManager>().Sounditem("Theme" ).volume=0;
        }
    }
    // void OnTriggerEnter2D(Collision2D col){
        
    // }
    
    void PlayerRaycast (){
        
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.down);
        if(hit.collider!=null && hit.distance<1.7 && hit.collider.tag=="Enemy"){

            myBody.AddForce(new Vector2(0f,70),ForceMode2D.Impulse);
            Player_Score.playerScore+=10;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,5),ForceMode2D.Impulse);
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled=false;
         

            hit.collider.gameObject.GetComponent<Enemy>().enabled=false;
            FindObjectOfType<SoundManager>().Play("Bump");
            
           

        } 

        RaycastHit2D uphit=Physics2D.Raycast(transform.position,Vector2.up);
            if(uphit.collider!=null && uphit.distance<1.5 && uphit.collider.tag=="QuestionBlock"){
                //Debug.Log("COllided");
                uphit.collider.GetComponent<QuestionBlock>().QuestionBlockBounce();
                
            }
            if(uphit.collider!=null && uphit.distance<1.5 && uphit.collider.tag=="Block"){
                FindObjectOfType<SoundManager>().Play("Brick");
            }
    }
}
