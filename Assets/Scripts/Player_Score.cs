using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player_Score : MonoBehaviour
{
    private float timeLeft=10;
    public static int playerScore=0;

    public GameObject timeLeftUI;
    public GameObject HighScoreUI;

    Text highScore;
    Text timeleft;
    Text playerscore;
    public GameObject playerScoreUI;
    void Start(){
        timeleft= timeLeftUI.GetComponent<Text>();
        playerscore= playerScoreUI.GetComponent<Text>();
        highScore=HighScoreUI.GetComponent<Text>();
    }

    void Update()
    {
        timeLeft-=Time.deltaTime;
        timeleft.text=("Time left :" + (int)timeLeft);
        if(timeLeft<0){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerScore=0;
        }
        playerscore.text=("Score  :" + (int)playerScore);
        highScore.text=("High Score:" + DataManager.datamanger.highScore);
        //Debug.Log(timeLeft);
        // if(timeLeft<0.2f){
        //     SceneManager.LoadScene("Main");
        // }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag=="Levelend"){
            CountScore();
            //SceneManager.LoadScene("Level");
        }
        if(collider.gameObject.tag=="Coin"){
            playerScore+=10;
            Destroy(collider.gameObject);
            FindObjectOfType<SoundManager>().Play("Coin");
        }
    }
    void CountScore(){
        //Debug.Log("Current High Score" + DataManager.datamanger.highScore);

        playerScore+= (int)(timeLeft*10);
        if(playerScore>DataManager.datamanger.highScore){
            DataManager.datamanger.highScore=playerScore;
        }
        DataManager.datamanger.SaveData();
        Debug.Log(DataManager.datamanger.highScore);
        //Debug.Log(playerScore);
    }
}
