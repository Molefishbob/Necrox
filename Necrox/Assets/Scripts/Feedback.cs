using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Feedback : MonoBehaviour {

    
    public Camera _camera;
    public GameField gameField;
    public int explosionLayer = 50;

    public GameObject mainChar;
    public GameObject enemy;
    public GameObject tileExplosion;
    public GameObject example;
    public GameObject fireball;
    public GameObject earthProtect;
    public GameObject waterHeal;
    public GameObject skeleton;
    public TMP_Text scoreText;
    public Canvas CombatUI;
    [SerializeField]
    private AudioClip _fireMatch;
    [SerializeField]
    private AudioClip _waterMatch;
    [SerializeField]
    private AudioClip _earthMatch;
    [SerializeField]
    private AudioClip _chaosMatch;

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
                CombatUI.GetComponent<CombatUI>().FireAttack();
                score += 6;
                break;
            case "water":
                waterCount++;
                CombatUI.GetComponent<CombatUI>().WaterHeal();
                score += 5;
                break;
            case "earth":
                earthCount++;
                CombatUI.GetComponent<CombatUI>().EarthProtect();
                score += 4;
                break;
            case "chaos":
                chaosCount++;
                CombatUI.GetComponent<CombatUI>().SkeletonAttack();
                score += 7;
                break;
        }
        if (fireCount >= 3) {
            //Debug.Log("firecount is: " + fireCount);
            Instantiate(fireball);
            mainChar.GetComponent<Animator>().SetTrigger("CastSpell");
            _camera.GetComponent<CameraManager>()
				   	.PlaySound(_fireMatch,GameManager._soundVolume,usePitchVariance: true);
            fireCount = 0;
            //mainChar.GetComponent<Animator>().SetBool("StartSpell", false);
        }
        if (waterCount >= 3) {
            Instantiate(waterHeal);
            mainChar.GetComponent<Animator>().SetTrigger("CastSpell");
            _camera.GetComponent<CameraManager>()
				   	.PlaySound(_waterMatch,GameManager._soundVolume,usePitchVariance: true);
            waterCount = 0;
        }
        if (earthCount >= 3) {
            Instantiate(earthProtect);
            mainChar.GetComponent<Animator>().SetTrigger("CastSpell");
            _camera.GetComponent<CameraManager>()
				   	.PlaySound(_earthMatch,GameManager._soundVolume,usePitchVariance: true);
            earthCount = 0;
        }
        if (chaosCount >= 3) {
            Instantiate(skeleton);
            mainChar.GetComponent<Animator>().SetTrigger("CastSpell");
            _camera.GetComponent<CameraManager>()
				   	.PlaySound(_chaosMatch,GameManager._soundVolume,usePitchVariance: true);
            chaosCount = 0;
        }
        scoreText.text ="Score:" + score;
    }

    public int GetScore() {
        return score;
    }
}
