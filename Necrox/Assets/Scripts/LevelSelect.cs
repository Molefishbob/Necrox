using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    private const string LevelFormat = "Level";
    private const string HighScoreText = "High Score:\n{0}";
    public TMP_Text highScoreText;
    public TMP_Text levelDisplay;
    public Image enemyDisplayed;
    public GameObject _lockSymbol;
    private string level;
    private int levelInt;

    public Sprite elder;
    public Sprite villagerMale;
    public Sprite villagerFemale;
    public Sprite femaleMinion;
    public Sprite minionRed;
    public Sprite minionGreen;
    public Sprite minionBlue;
    public Sprite elderTree;
    public Sprite king;
    private bool _unlocked;

    void Awake () {
        
        levelInt = GameManager.GetLatestLevel();
        level = LevelFormat + levelInt;
        highScoreText.SetText(GameManager.GetHighScore(level).ToString());
        levelDisplay.text = LevelFormat + " " + levelInt;
        SetEnemySprite(levelInt);
        string lastLevel = LevelFormat + (levelInt-1);
        if (levelInt == 1 || GameManager.GetHighScore(lastLevel) != 0 || GameManager.GetHighScore(LevelFormat + levelInt) != 0) {
            _unlocked = true;
        } else {
            _unlocked = false;
            _lockSymbol.SetActive(true);
        }
    }
	
	
	void Update () {
		
	}

    public void NextLevel()
    {
        int nextLevelInt = 0;
        if (levelInt < 15)
        {
            nextLevelInt = levelInt + 1;
        }
        string nextlevel = LevelFormat + nextLevelInt;
        if (GameManager.GetHighScore(level) != 0 || GameManager.GetHighScore(LevelFormat + levelInt) != 0) {
            _unlocked = true;
            _lockSymbol.SetActive(false);
            levelInt = nextLevelInt;
            level = nextlevel;
            highScoreText.SetText(GameManager.GetHighScore(level).ToString());
            SetEnemySprite(levelInt);
            levelDisplay.text = LevelFormat + " " + levelInt;
        } else {
            if (!_lockSymbol.activeSelf) {

                levelInt = nextLevelInt;
                level = nextlevel;
                levelDisplay.text = LevelFormat + " " + nextLevelInt;

            }
            _unlocked = false;
            _lockSymbol.SetActive(true);
        }
        //need to change the character
    }

    private void SetEnemySprite(int levelInt)
    {
        switch (levelInt)
        {
            case 1: enemyDisplayed.sprite = minionRed; break;
            case 2: enemyDisplayed.sprite = villagerMale; break;
            case 3: enemyDisplayed.sprite = minionGreen; break;
            case 4: enemyDisplayed.sprite = villagerFemale; break;
            case 5: enemyDisplayed.sprite = minionBlue; break;
            case 6: enemyDisplayed.sprite = villagerFemale; break;
            case 7: enemyDisplayed.sprite = elder; break;
            case 8: enemyDisplayed.sprite = femaleMinion; break;
            case 9: enemyDisplayed.sprite = villagerMale; break;
            case 10: enemyDisplayed.sprite = elder; break;
            case 11: enemyDisplayed.sprite = femaleMinion; break;
            case 12: enemyDisplayed.sprite = minionRed; break;
            case 13: enemyDisplayed.sprite = villagerMale; break;
            case 14: enemyDisplayed.sprite = elderTree; break;
            case 15: enemyDisplayed.sprite = king; break;
        }
    }

    public void PreviousLevel() {
        
        if (levelInt > 1) {
        
            levelInt -= 1;
        
        }
        
        level = LevelFormat + levelInt;
        highScoreText.SetText(GameManager.GetHighScore(level).ToString());
        _unlocked = true;
        _lockSymbol.SetActive(false);
        levelDisplay.text = LevelFormat + " " + levelInt;
        SetEnemySprite(levelInt);

    }

    public void StartLevel() {
        //Select the level based on the name
        if (_unlocked) {
        GameManager.SaveLatestLevel(levelInt);
        GameStateManager.Instance.ChangeState(
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , level));
        }
    }
}
