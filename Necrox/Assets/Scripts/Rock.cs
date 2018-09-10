using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private float speed = 0.018f;
    private BoxCollider2D coll;
    private bool onMove;
    private float yValue;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D>();
        onMove = true;
        switch (pos[1]) {

            case 0: yValue = 0.5f;
            break;

            case 1: yValue = -0.5f;
            break;

            case 2: yValue = -1.5f;
            break;

            case 3: yValue = -2.5f;
            break;

            case 4: yValue = -3.5f;
            break;

            case 5: yValue = -4.5f;
            break;

            case 6: yValue = -5.5f;
            break;

            case 7: yValue = -6.5f;
            break;

            default: Debug.Log("MISTAKES MISTAKES.");
            break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position.y >= yValue) {
            
            onMove = false;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            if (coll == Physics2D.OverlapPoint(touchPos)) {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
            }
        }

    }

	public void Init(int x, int y) {
        int[] pos = new int[] {x,y};
		this.pos = pos;
	}
}