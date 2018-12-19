using System.Collections;
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
    public GameObject EarthShieldPrefab;
    private GameObject EarthShield;
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
    [SerializeField]
    private AudioClip _buttonClick;

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
        LevelSettings(val);
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
    public void PlayClickAudio() {
			_camera.GetComponent<CameraManager>()
					.PlaySound(_buttonClick,GameManager._soundVolume,usePitchVariance: false);
		}

    public void FireAttack() {
        EnemyHealth.value -= FireDmg * ((float)(GameManager.defaultMultiplier + GameManager.comboCount*10)/100);
    }
    public void WaterHeal() {
        MainCharHealth.value += waterHeal * ((float)(GameManager.defaultMultiplier + GameManager.comboCount*10)/100);
    }
    public void EarthProtect() {
        if (!earthBool) {
            EarthShield = Instantiate(EarthShieldPrefab);
        }
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
                Destroy(EarthShield);
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
    public void LevelSettings(int CurrentLvl) {
        switch (CurrentLvl) {
            case 1:
                EnemyHealth.maxValue = 100;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 8.0f;
                enemyDmg = 10;
                break;
            case 2:
                EnemyHealth.maxValue = 125;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 6;
                enemyDmg = 9;
                break;
            case 3:
                EnemyHealth.maxValue = 150;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 5;
                enemyDmg = 8;
                break;
            case 4:
                EnemyHealth.maxValue = 200;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 12;
                enemyDmg = 20;
                break;
            case 5:
                EnemyHealth.maxValue = 175;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 4;
                enemyDmg = 8;
                break;
            case 6:
                EnemyHealth.maxValue = 200;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 7;
                enemyDmg = 15;
                break;
            case 7:
                EnemyHealth.maxValue = 300;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 15;
                enemyDmg = 30;
                break;
            case 8:
                EnemyHealth.maxValue = 225;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 10;
                enemyDmg = 20;
                break;
            case 9:
                EnemyHealth.maxValue = 250;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 7;
                enemyDmg = 15;
                break;
            case 10:
                EnemyHealth.maxValue = 275;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 8;
                enemyDmg = 20;
                break;
            case 11:
                EnemyHealth.maxValue = 200;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 3;
                enemyDmg = 7;
                break;
            case 12:
                EnemyHealth.maxValue = 80;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 2;
                enemyDmg = 10;
                break;
            case 13:
                EnemyHealth.maxValue = 325;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 7;
                enemyDmg = 20;
                break;
            case 14:
                EnemyHealth.maxValue = 300;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 11;
                enemyDmg = 35;
                break;
            case 15:
                EnemyHealth.maxValue = 400;
                EnemyHealth.value = EnemyHealth.maxValue;
                enemyAtkTime = 6;
                enemyDmg = 30;
                break;
        }

    }
}
