using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
    private const string HighScoreText = "HIGHSCORE:\n{0}";
    private const string ScoreText = "SCORE:\n{0}";
    private const string Defeat = "DEFEAT";
    private const string RestartText = "TRY AGAIN";
    private const string MainMenu = "MainMenu";
    private const float VictoryRed = 0.9411764705882353f;
    private const float VictoryGreen = 0.7843137254901961f;
    private const float VictoryBlue = 0.1568627450980392f;
    private const float DefeatRed = 0.7137254901960784f;
    private const float DefeatGreen = 0.0274509803921569f;
    private const float DefeatBlue = 0.0274509803921569f;
    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text StatusText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private TMP_Text newText;
    [SerializeField]
    [Tooltip("Level1,Level2,Level3,etc.")]
    private string _nextScene;
    private string state;
    private int _score;
    private int _currentScore;

    // Use this for initialization
    void Start () {
        SetScore(0);
	}
	
	// Update is called once per frame
	void Update () {
		_currentScore += _score/100*15;
        if (_currentScore >= _score) {
            _currentScore = _score;
            _scoreText.SetText(ScoreText, _currentScore);
        }

	}

    public void SetScore(int score) {
    
            if (GameManager.LevelEnd(SceneManager.GetActiveScene().name,score) && state != Defeat) {
    
                newText.gameObject.SetActive(true);
                StatusText.color = new Color(VictoryRed, VictoryGreen, VictoryBlue);
                highScoreText.SetText(HighScoreText, score);
    
            } else if (state == Defeat) {
                StatusText.color = new Color(DefeatRed, DefeatGreen, DefeatBlue);
                _continueButton.gameObject.GetComponentInChildren<TMP_Text>().SetText(RestartText); 

                } else {
                    
                StatusText.color = new Color(VictoryRed, VictoryGreen, VictoryBlue);
                highScoreText.SetText(HighScoreText, GameManager.GetHighScore(SceneManager.GetActiveScene().name));
            
                }
        _score = score;
    }
    public void SetState(string result) {
        StatusText.text = result;
        state = result;
        
    }

    public void Continue() {
        if (state == Defeat) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            Debug.Log(GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , _nextScene)));
            
        }
    }
    public void ToMainMenu() {
        GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , MainMenu));
    }
}
