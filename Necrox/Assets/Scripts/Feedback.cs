using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour {

    
    public GameField gameField;
    public int explosionLayer = 50;

    public GameObject tileExplosion;
    public GameObject example;
    public GameObject fireball;
    public GameObject earthProtect;
    public GameObject waterHeal;
    public GameObject skeleton;
    public Text scoreText;
    public Canvas CombatUI;

    private int fireCount;
    private int waterCount;
    private int earthCount;
    private int chaosCount;
    private int score = 0;

    private int countCount = 0;

    private RaycastHit2D hit;
    private GameObject testRock;


    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        // if (Input.GetMouseButtonDown(0)) {

        //     Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector2 clickPos = new Vector2(wp.x, wp.y);
        //     hit = Physics2D.Raycast(clickPos, -Vector2.up);

        //     if (hit.collider != null) {

        //         testRock = hit.collider.gameObject;
        //         TestFeedback(testRock);
        //     }
        // }
    }

    public void TestFeedback (GameObject tile) {
        GameObject test = Instantiate(example, new Vector3(tile.transform.position.x, tile.transform.position.y, 0), Quaternion.identity);
        //test.gameObject.GetComponent<SpriteRenderer>().sortingOrder = explosionLayer;

        //how to thrown in the elemental if needed later
        switch (tile.GetComponent<Rock>()._element) {
            case "fire":
                Instantiate(fireball);
                break;
        }
    }

    public void TileFeedback (Vector3 tilePos, GameObject OgTile) {
       /* countCount++;
        Debug.Log("how many times did it get here: " + countCount);*/

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
            waterCount = 0;
        }
        if (earthCount >= 3) {
            Instantiate(earthProtect);
            earthCount = 0;
        }
        if (chaosCount >= 3) {
            Instantiate(skeleton);
            chaosCount = 0;
        }
        scoreText.text = "Score:" + score;
    }
}
