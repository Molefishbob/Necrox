using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;

public class GameOverMenu : MonoBehaviour {

    
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text StatusText;
    [SerializeField]
    [Tooltip("Level1,Level2,Level3,etc.")]
    private string _nextScene;
    public string nextSceneToLoad;
    private string state;

    // Use this for initialization
    void Start () {
        SetScore(640);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int score) {
        _scoreText.SetText("SCORE: {0}", score);
    }
    public void SetState(string result) {
        StatusText.text = result;
        state = result;
        
    }

    public void Continue() {
        if(!GameStateManager.Instance.ChangeState( 
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , _nextScene))) {
            GameStateManager.Instance.ChangeState( GameStateType.Victory );
        }
        if (state == "DEFEAT") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            SceneManager.LoadScene(nextSceneToLoad);
        }
    }
}
