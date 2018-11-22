﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Feedback : MonoBehaviour {

    
    public GameField gameField;
    public int explosionLayer = 50;

    public GameObject tileExplosion;
    public GameObject example;
    public GameObject fireball;
    public GameObject earthProtect;
    public GameObject waterHeal;
    public GameObject skeleton;
    public TMP_Text scoreText;
    public Canvas CombatUI;

    private int fireCount;
    private int waterCount;
    private int earthCount;
    private int chaosCount;
    private int score = 0;

    private RaycastHit2D hit;
    private GameObject testRock;

    void Start () {
		
	}

    void Update() {
        
    }

    public void TileFeedback (Vector3 tilePos, GameObject OgTile) {

        SpellFeedback(OgTile);
        if (OgTile.transform.childCount > 0) {
            Destroy(OgTile.transform.GetChild(0).gameObject);
        }
        GameObject explosion = Instantiate(tileExplosion, tilePos,  Quaternion.identity);
        explosion.transform.parent = OgTile.transform;
        //explosion.gameObject.GetComponent<SpriteRenderer>().sortingOrder = explosionLayer;
    }

    void SpellFeedback (GameObject tile) {
        switch (tile.GetComponent<Rock>()._element) {
            case "fire":
                fireCount++;
                score += 5;
                break;
            case "water":
                waterCount++;
                score += 5;
                break;
            case "earth":
                earthCount++;
                score += 5;
                break;
            case "chaos":
                chaosCount++;
                score += 5;
                break;
        }
        if (fireCount >= 3) {
            Instantiate(fireball);
            CombatUI.GetComponent<CombatUI>().FireAttack();
            fireCount = 0;
        }
        if (waterCount >= 3) {
            Instantiate(waterHeal);
            CombatUI.GetComponent<CombatUI>().WaterHeal();
            waterCount = 0;
        }
        if (earthCount >= 3) {
            Instantiate(earthProtect);
            CombatUI.GetComponent<CombatUI>().EarthProtect();
            earthCount = 0;
        }
        if (chaosCount >= 3) {
            Instantiate(skeleton);
            CombatUI.GetComponent<CombatUI>().SkeletonAttack();
            chaosCount = 0;
        }
        scoreText.text ="SCORE:" + score;
    }

    public int GetScore() {
        return score;
    }
}
