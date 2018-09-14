using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
	
    private BoxCollider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ShittyMovement() {
		
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            if (coll == Physics2D.OverlapPoint(touchPos)) {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
            }
        }
	}
}
