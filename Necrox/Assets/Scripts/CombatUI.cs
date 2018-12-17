﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatUI : MonoBehaviour {
    private const string LevelText = "Level {0}";
    public Slider MainCharHealth;
    public Slider EnemyHealth;
    public Canvas GameOverMenu;
    public GameObject Feedback;
    public GameObject enemy;
    public GameObject enemyDmgFeedback;

    private float FireDmg = 3;
    private float waterHeal = 2;
    private bool earthBool = false;
    private float earthPercentage = .04f;
    private float earthStack;
    private float earthMax = .5f;
    private float skeleDmg = 4;

    public float enemyAtkTime;
    public float enemyDmg;
    public AudioClip _victoryMusic;
    public Camera _camera;
    public TMP_Text _currentLevel;
    private bool _victory;
    private bool firstAttack = true;
    private bool attackComplete = true;
    private bool _defeat;
    public AudioClip _loserBassAudio;
    public AudioClip _winnerBassAudio;

    public bool _paused {
        get;
        set;
    }
    

    /*
     * Have two health bars
     * method for each of the spells/matches to add or subtract health
     * if Enemy health = 0 win 
     * if Char health = 0 lose
     * show the score scene after and set the game to be unplayable
     */
    // Use this for initialization
    void Start () {
        int val = 0;
        System.Int32.TryParse(SceneManager.GetActiveScene().name.Substring(5),out val);
        _currentLevel.SetText(LevelText, val);
	}
	
	void Update () {
        if (!_paused) {
            //Debug.Log(EnemyHealth.value);
            if (EnemyHealth.value <= 0 && !_victory && !_defeat) {

                _camera.GetComponent<AudioSource>().Stop();
                
                _camera.GetComponent<CameraManager>()
				   	.PlaySound(_winnerBassAudio,GameManager._soundVolume,usePitchVariance: true);

                FindObjectOfType<GameLogic>()._paused = true;
                GameOverMenu.GetComponent<GameOverMenu>().SetState("VICTORY");
                GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
                GameOverMenu.gameObject.SetActive(true);
                _victory = true;
            }
            if (MainCharHealth.value <= 0 && !_victory && !_defeat) {

                FindObjectOfType<GameLogic>()._paused = true;

                _camera.GetComponent<AudioSource>().Stop();

                _camera.GetComponent<CameraManager>()
				   	.PlaySound(_loserBassAudio,GameManager._soundVolume,usePitchVariance: true);

                GameOverMenu.GetComponent<GameOverMenu>().SetState("DEFEAT");
                GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
                GameOverMenu.gameObject.SetActive(true);
                _defeat = true;
            }
            if (MainCharHealth.value > 0 && EnemyHealth.value > 0) {
                EnemyAttack();
            }
        }
    }

    public void FireAttack() {
        EnemyHealth.value -= FireDmg * ((float)(GameManager.defaultMultiplier + GameManager.comboCount*10)/100);
    }
    public void WaterHeal() {
        MainCharHealth.value += waterHeal * ((float)(GameManager.defaultMultiplier + GameManager.comboCount*10)/100);
    }
    public void EarthProtect() {
        if (earthStack >= earthMax) {
            earthStack = earthMax;
        } else {
            earthStack += earthPercentage;
        }
        earthBool = true; 
    }
    public void SkeletonAttack() {
        EnemyHealth.value -= skeleDmg * ((float)(GameManager.defaultMultiplier + GameManager.comboCount*10)/100);
    }

    void EnemyAttack() {
        if (firstAttack) {
            firstAttack = false;
        }
        if (attackComplete) {
            attackComplete = false;
            gameObject.GetComponent<Timer>().SetTime(enemyAtkTime);
            gameObject.GetComponent<Timer>().StartTimer();
        }
        if(gameObject.GetComponent<Timer>().IsCompleted) {

            _camera.GetComponent<CameraManager>()
				   	.PlaySound(enemy.GetComponent<WeaponSound>()._weaponSound,GameManager._soundVolume,usePitchVariance: true);

            attackComplete = true;
            enemy.GetComponent<Animator>().SetTrigger("Attack");
            if (earthBool) {
                earthBool = false;
                earthStack = 0;
                MainCharHealth.value -= (float)(enemyDmg * (1 - earthStack));
                Instantiate(enemyDmgFeedback);
            } else {
                MainCharHealth.value -= enemyDmg;
                Instantiate(enemyDmgFeedback);
            }
            
        }
    }
}
