using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject greetings;
    public GameObject matchExample;
    public GameObject fire;
    public GameObject water;
    public GameObject earth;
    public GameObject chaos;
    public GameObject complete;
    public GameObject fireball;
    public GameObject earthWall;
    public GameObject waterball;
    public GameObject skeleton;
    public GameObject skipMessage;
    public TMP_Text frwrdButton;

    private int tutorialCount = 1;
    private bool skipCheck = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Forward() {
        if (frwrdButton.text == "START") {
            gameObject.SetActive(false);
        }
        if (tutorialCount < 7) {
            tutorialCount++;
        }
        switch (tutorialCount) {
            case 1:
                //show greetings hide match 3
                greetings.SetActive(true);
                matchExample.SetActive(false);
                break;
            case 2:
                greetings.SetActive(false);
                matchExample.SetActive(true);
                break;
            case 3:
                fire.SetActive(true);
                matchExample.SetActive(false);
                fireball.SetActive(true);
                break;
            case 4:
                fire.SetActive(false);
                fireball.SetActive(false);
                water.SetActive(true);
                waterball.SetActive(true);
                break;
            case 5:
                water.SetActive(false);
                waterball.SetActive(false);
                earth.SetActive(true);
                earthWall.SetActive(true);
                break;
            case 6:
                earth.SetActive(false);
                earthWall.SetActive(false);
                chaos.SetActive(true);
                skeleton.SetActive(true);
                frwrdButton.text = "NEXT";
                break;
            case 7:
                chaos.SetActive(false);
                skeleton.SetActive(false);
                complete.SetActive(true);
                frwrdButton.text = "START";
                break;
        }
    }

    public void Backwards() {
        if (skipCheck) {
            skipMessage.SetActive(false);
            frwrdButton.text = "NEXT";
            skipCheck = false;
        } else {
            if (tutorialCount > 1) {
                tutorialCount--;
            }
        }
        
        switch (tutorialCount) {
            case 1:
                //show greetings hide match 3
                greetings.SetActive(true);
                matchExample.SetActive(false);
                break;
            case 2:
                greetings.SetActive(false);
                matchExample.SetActive(true);
                break;
            case 3:
                fire.SetActive(true);
                matchExample.SetActive(false);
                fireball.SetActive(true);
                break;
            case 4:
                fire.SetActive(false);
                fireball.SetActive(false);
                water.SetActive(true);
                waterball.SetActive(true);
                break;
            case 5:
                water.SetActive(false);
                waterball.SetActive(false);
                earth.SetActive(true);
                earthWall.SetActive(true);
                break;
            case 6:
                earth.SetActive(false);
                earthWall.SetActive(false);
                chaos.SetActive(true);
                skeleton.SetActive(true);
                frwrdButton.text = "NEXT";
                break;
            case 7:
                chaos.SetActive(false);
                skeleton.SetActive(false);
                complete.SetActive(true);
                frwrdButton.text = "START";
                break;
        }
    }
    public void SkipTutorial() {
        skipCheck = true;
        frwrdButton.text = "START";
        skipMessage.SetActive(true);
        skeleton.SetActive(false);
        earthWall.SetActive(false);
        waterball.SetActive(false);
        fireball.SetActive(false);
    }
}
