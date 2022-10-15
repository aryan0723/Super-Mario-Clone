using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DataManager datamanger;
    public int highScore=0;
    void Start(){
        DataManager.datamanger.LoadData();
    }


    void Awake(){
        if(datamanger == null){
            DontDestroyOnLoad(gameObject);
            datamanger=this;    
        }
        else if(datamanger!=this){
            Destroy(gameObject);
        }          
        
    }
    public void SaveData(){
        BinaryFormatter BinForm= new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        gameData data=new gameData();
        data.highscore=highScore;
        BinForm.Serialize(file,data);
        file.Close();
    }
    public void LoadData(){
        if(File.Exists(Application.persistentDataPath + "/gameInfo.dat")){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file=File.Open(Application.persistentDataPath + "/gameInfo.dat",FileMode.Open);
            gameData data=(gameData)binaryFormatter.Deserialize(file);
            file.Close();
            highScore=data.highscore;
            
        }
    }
}
[Serializable]
class gameData{
    public int highscore;
}
