using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;

public class GameOverMenu : MonoBehaviour {
    private const string HighScoreText = "HIGHSCORE:\n{0}";
    private const string ScoreText = "SCORE:\n{0}";
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

    // Use this for initialization
    void Start () {
        SetScore(640);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int score) {
        if (GameManager.LevelEnd(SceneManager.GetActiveScene().name,score)) {
            newText.gameObject.SetActive(true);
            highScoreText.SetText(HighScoreText, score);
        } else {
            highScoreText.SetText(HighScoreText, GameManager.GetHighScore(SceneManager.GetActiveScene().name));
        } 
        _scoreText.SetText(ScoreText, score);
    }
    public void SetState(string result) {
        StatusText.text = result;
        state = result;
        
    }

    public void Continue() {
        if (state == "DEFEAT") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            Debug.Log(GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , _nextScene)));
            
        }
    }
}
