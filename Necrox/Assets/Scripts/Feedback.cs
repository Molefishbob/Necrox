using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour {

    
    public GameField gameField;
    public int explosionLayer = 50;

    public GameObject tileExplosion;
    public GameObject fireball;

    private RaycastHit2D hit;
    private GameObject testRock;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(wp.x, wp.y);
            hit = Physics2D.Raycast(clickPos, -Vector2.up);

            if (hit.collider != null) {

                testRock = hit.collider.gameObject;
                TileFeedback(testRock);
            }
        }
	}

    public void TileFeedback (GameObject tile) {
        GameObject explosion = Instantiate(tileExplosion, new Vector3(tile.transform.position.x, tile.transform.position.y, 0),  Quaternion.identity);
        //explosion.gameObject.GetComponent<SpriteRenderer>().sortingOrder = explosionLayer;

        //how to thrown in the elemental if needed later
        switch (tile.GetComponent<Rock>()._element) {
            case "fire":
                Instantiate(fireball);
                break; 
        }
    }
}
