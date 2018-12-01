using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    private const string LevelFormat = "Level";
    public TMP_Text highScoreText;
    public TMP_Text levelDisplay;
    public Image enemyDisplayed;
    private string level;
    private int levelInt;

    public Sprite elder;
    public Sprite villagerMale;
    public Sprite villagerFemale;
    public Sprite minionRed;
    public Sprite minionGreen;
    public Sprite minionBlue;
    public Sprite elderTree;


    void Start () {
        //select the latest level
        //code for that
        levelInt = 5;
        level = LevelFormat + levelInt;
        //highScoreText.SetText(GameManager.GetHighScore(SceneManager.GetSceneByName(level)));
    }
	
	
	void Update () {
		
	}

    public void NextLevel() {
        if (levelInt < 15) {
            levelInt += 1;
        }
        level = LevelFormat + levelInt;
        levelDisplay.text = "Level " + levelInt;
        //need to change the character
        switch (levelInt) {
            case 1: enemyDisplayed.sprite = minionRed; break;
            case 2: enemyDisplayed.sprite = villagerMale; break;
            case 3: enemyDisplayed.sprite = minionGreen; break;
            case 4: enemyDisplayed.sprite = villagerFemale; break;
            case 5: enemyDisplayed.sprite = minionBlue; break;
            case 6: enemyDisplayed.sprite = villagerMale; break;
            case 7: enemyDisplayed.sprite = elder; break;
            case 8: enemyDisplayed.sprite = villagerFemale; break;
            case 9: enemyDisplayed.sprite = villagerMale; break;
            case 10: enemyDisplayed.sprite = elder; break;
            case 11: enemyDisplayed.sprite = minionGreen; break;
            case 12: enemyDisplayed.sprite = minionRed; break;
            case 13: enemyDisplayed.sprite = villagerMale; break;
            case 14: enemyDisplayed.sprite = elderTree; break;
            case 15: enemyDisplayed.sprite = elderTree; break;
        }
        
    }

    public void PreviousLevel() {
        if (levelInt > 1) {
            levelInt -= 1;
        }
        level = LevelFormat + levelInt;
        levelDisplay.text = "Level " + levelInt;
        switch (levelInt) {
            case 1: enemyDisplayed.sprite = minionRed; break;
            case 2: enemyDisplayed.sprite = villagerMale; break;
            case 3: enemyDisplayed.sprite = minionGreen; break;
            case 4: enemyDisplayed.sprite = villagerFemale; break;
            case 5: enemyDisplayed.sprite = minionBlue; break;
            case 6: enemyDisplayed.sprite = villagerMale; break;
            case 7: enemyDisplayed.sprite = elder; break;
            case 8: enemyDisplayed.sprite = villagerFemale; break;
            case 9: enemyDisplayed.sprite = villagerMale; break;
            case 10: enemyDisplayed.sprite = elder; break;
            case 11: enemyDisplayed.sprite = minionGreen; break;
            case 12: enemyDisplayed.sprite = minionRed; break;
            case 13: enemyDisplayed.sprite = villagerMale; break;
            case 14: enemyDisplayed.sprite = elderTree; break;
            case 15: enemyDisplayed.sprite = elderTree; break;
        }
    }

    public void StartLevel() {
        //Select the level based on the name
        GameStateManager.Instance.ChangeState(
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , level));
    }
}
