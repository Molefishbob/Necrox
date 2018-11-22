﻿using System.Collections;
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
    public float enemyHealth;

    /*
     * Have two health bars
     * method for each of the spells/matches to add or subtract health
     * if Enemy health = 0 win 
     * if Char health = 0 lose
     * show the score scene after and set the game to be unplayable
     */
	// Use this for initialization
	void Start () {
        MainCharHealth.value = 50;
        Debug.Log(MainCharHealth.value + "   " + EnemyHealth.value);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(EnemyHealth.value);
		if (EnemyHealth.value <= 0) {
            FindObjectOfType<GameLogic>().SetTouchFalse();
            GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
            GameOverMenu.gameObject.SetActive(true);
        }
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
}
