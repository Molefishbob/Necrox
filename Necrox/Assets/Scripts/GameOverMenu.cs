using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour {

    
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text StatusText;

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

    public void Continue() {
        SceneManager.LoadScene(1);
    }
}
