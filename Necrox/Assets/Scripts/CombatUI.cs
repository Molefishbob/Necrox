using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

    public Slider MainCharHealth;
    public Slider EnemyHealth;
    public Canvas GameOverMenu;
    public GameObject Feedback;

    public float FireDmg = 10;
    public float waterHeal = 7;
    public bool earthBool = false;
    public float skeleDmg = 15;

    public float enemyAtkTime;
    public float enemyDmg;
    private bool firstAttack = true;
    private bool attackComplete = true;

    /*
     * Have two health bars
     * method for each of the spells/matches to add or subtract health
     * if Enemy health = 0 win 
     * if Char health = 0 lose
     * show the score scene after and set the game to be unplayable
     */
	void Start () {
        MainCharHealth.value = 50;
        Debug.Log(MainCharHealth.value + "   " + EnemyHealth.value);
	}
	
	void Update () {
        //Debug.Log(EnemyHealth.value);
		if (EnemyHealth.value <= 0) {
            FindObjectOfType<GameLogic>().SetTouchFalse();
            GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
            GameOverMenu.gameObject.SetActive(true);
        }
        if (MainCharHealth.value <= 0) {
            FindObjectOfType<GameLogic>().SetTouchFalse();
            GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
            GameOverMenu.gameObject.SetActive(true);
        }
        EnemyAttack();
    }

    public void FireAttack() {
        EnemyHealth.value -= FireDmg;
    }
    public void WaterHeal() {
        MainCharHealth.value += waterHeal;
    }
    public void EarthProtect() {
        earthBool = true; 
    }
    public void SkeletonAttack() {
        EnemyHealth.value -= skeleDmg;
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
            attackComplete = true;
            MainCharHealth.value -= enemyDmg;
        }
    }
}
