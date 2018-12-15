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
    private const float ScorePercentage = 0.005f;
    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private Image _background;
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
    private bool _statusFly;
    private bool _countScore;
    private int _score;
    private int _currentScore;
    private bool _scoreCounted;
    private int _oldHighScore;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (_statusFly) {
            LeanTween.scale(StatusText.gameObject,Vector2.one,0.5f);
            LeanTween.moveLocalY(StatusText.gameObject,200f,0.5f);
            if (StatusText.gameObject.transform.localScale.x > 0.95f) {
                _statusFly = false;
                _countScore = true;
                _scoreText.gameObject.SetActive(true);
                highScoreText.gameObject.SetActive(true);
            }
        }

        if (_countScore) {
            _background.gameObject.SetActive(true);
            _currentScore += (int) (_score * ScorePercentage)+1;

            if (Input.touchCount == 1) {
                _currentScore = _score;
            }

            if (_currentScore < _score) {
                _scoreText.SetText(ScoreText, _currentScore);

            } else {
                _currentScore = _score;
                _scoreText.SetText(ScoreText,_currentScore);
                _countScore = false;
                _scoreCounted = true;
            }
            if (_currentScore > _oldHighScore && state != Defeat) {
                
                Debug.Log("New highscore");
                newText.gameObject.SetActive(true);
                highScoreText.SetText(HighScoreText, _currentScore);
                _oldHighScore = _currentScore;

                } 

        }

        if (_scoreCounted) {
            _continueButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);
        }

	}

    public void SetScore(int score) {
        _scoreCounted = false;
        _oldHighScore = (int) GameManager.GetHighScore(SceneManager.GetActiveScene().name);
        _score = score;

        if (state != Defeat) {
            if (GameManager.LevelEnd(SceneManager.GetActiveScene().name,score)) {

            Debug.Log("VICTORY");
            StatusText.color = new Color(VictoryRed, VictoryGreen, VictoryBlue);
        
            }

            StatusText.color = new Color(VictoryRed, VictoryGreen, VictoryBlue);
            highScoreText.SetText(HighScoreText, _oldHighScore);

        } else if (state == Defeat) {
            Debug.Log("DEFEAT");
            StatusText.color = new Color(DefeatRed, DefeatGreen, DefeatBlue);
            _continueButton.gameObject.GetComponentInChildren<TMP_Text>().SetText(RestartText); 

            }
    }

    public void SetState(string result) {
        StatusText.text = result;
        state = result;
        _statusFly = true;
        
    }

    public void Continue() {
        if (state == Defeat) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            int val = 0;
            if (_nextScene != MainMenu) {
                System.Int32.TryParse(_nextScene.Substring(5), out val);
                GameManager.SaveLatestLevel(val);
            }
            GameManager.comboCount = 0;
            Debug.Log(GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , _nextScene)));
            
        }
    }
    public void ToMainMenu() {
        GameManager.comboCount = 0;
        GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , MainMenu));
    }
}
