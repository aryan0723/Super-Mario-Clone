using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    private bool isDied;
    // Start is called before the first frame update
    void Start()
    {
        isDied=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y<-5){
            isDied=true;
        }
    }
    private void LateUpdate() {
        if(isDied==true){
            StartCoroutine("Die");
        }
        
    }
    IEnumerator Die(){
        Destroy(gameObject);
        yield return null;
    }
}
