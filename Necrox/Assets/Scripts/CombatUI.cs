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
    public float WaterHeal = 7;
    public bool earthProtect = false;
    public float SkeleDmg = 15;

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
            GameOverMenu.gameObject.SetActive(true);
            GameOverMenu.GetComponent<GameOverMenu>().SetScore(Feedback.GetComponent<Feedback>().GetScore());
        }
	}

    public void FireAttack() {
        //enemyHP -= FireDmg;
        EnemyHealth.value -= FireDmg;
    }
}
