using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame ()

    {
        DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}

//Another way of doing the game loader
//public class GameLoader : monoBehaviour { 

//  public void LoadScene ()
//  {  
//      SceneManager.LoadScene(1) 
//  } 