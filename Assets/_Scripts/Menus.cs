using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public void LoadScene(int index){
        LoadSceneStatic(index);
    }

    public static void LoadSceneStatic(int index){
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
