using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public Text loseText;
    void Start(){
        if(loseText!=null){
            loseText.text = "Your Score: "+PlayerPrefs.GetInt("Score",0);
        }
    }

    public void LoadScene(int index){
        LoadSceneStatic(index);
    }

    public static void LoadSceneStatic(int index){
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        
    }

    public void LoadLose(){

    }
}
