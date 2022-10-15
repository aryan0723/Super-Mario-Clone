using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public float bounceHeight=1;
    public float bounceSpeed=10;

    public float coinSpeed=15;
    public float coinHeight =4;
    public float coinFalldistance=2;
    private bool canBounce=true;
    private Vector2 originalPos;

    public Sprite emptyBlockSprite;

    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        originalPos=transform.localPosition;

    }
    public void QuestionBlockBounce(){
        if(canBounce==true){
            canBounce=false;
            StartCoroutine(Bounce());
            FindObjectOfType<SoundManager>().Play("Brick");
            
            PresentCoin();
        }
    }
    void ChangeSprite(){
        //GetComponent<Animator>().enabled=false;
        GetComponent<SpriteRenderer>().sprite=emptyBlockSprite;
    }
    void PresentCoin(){
        GameObject spinningCoin=(GameObject)Instantiate (coin);
        spinningCoin.transform.SetParent(this.transform.parent);
        spinningCoin.transform.localPosition=new Vector2(originalPos.x,originalPos.y+1);
        //Debug.Log("Coin Coroutine");
        StartCoroutine(MoveCoin(spinningCoin));
        FindObjectOfType<SoundManager>().Play("Coin");
    }
    IEnumerator Bounce(){
        ChangeSprite();
        
        //Debug.Log("Bounce Started :");
        while(true){
            transform.localPosition=new Vector2(transform.localPosition.x,transform.localPosition.y+bounceSpeed*Time.deltaTime);
            if(transform.localPosition.y>=originalPos.y + bounceHeight){
                break;
            }
            yield return null;
        }
        while(true){
            transform.localPosition=new Vector2 (transform.localPosition.x ,transform.localPosition.y-bounceSpeed*Time.deltaTime);
            if(transform.localPosition.y<=originalPos.y){
                transform.localPosition=originalPos;
                break;
            }
            yield return null ;
        }
        
    }
    IEnumerator MoveCoin(GameObject coin ){
        while(true){
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x,coin.transform.localPosition.y+coinSpeed*Time.deltaTime);
            if(coin.transform.localPosition.y>=originalPos.y+coinHeight+1){
                break;
            }
            yield return null;
        }
        while(true){
            coin.transform.localPosition=new Vector2(coin.transform.localPosition.x,coin.transform.localPosition.y-coinSpeed*Time.deltaTime);
            if(coin.transform.localPosition.y<=originalPos.y+coinFalldistance+1){
                Destroy(coin.gameObject);
                break;
            }
            yield return null;
        }

    }
}
