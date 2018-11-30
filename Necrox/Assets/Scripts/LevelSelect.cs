using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public TMP_Text highScoreText;
    public TMP_Text levelDisplay;
    public Image enemyDisplayed;
    private string level;
    private int levelInt;


    void Start () {
        //select the latest level
        //code for that
        levelInt = 5;
        level = "level" + levelInt;
        //highScoreText.SetText(GameManager.GetHighScore(SceneManager.GetSceneByName(level)));
    }
	
	
	void Update () {
		
	}

    public void NextLevel() {
        if (levelInt < 15) {
            levelInt += 1;
        }
        level = "Level" + levelInt;
        Debug.Log(level);
        levelDisplay.text = "Level " + levelInt;

    }

    public void PreviousLevel() {
        if (levelInt > 1) {
            levelInt -= 1;
        }
        levelDisplay.text = "Level " + levelInt;
    }

    public void StartLevel() {
        //Select the level based on the name
        GameStateManager.Instance.ChangeState(
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , level));
    }
}
