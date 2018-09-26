using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
	
    private BoxCollider2D coll;
    private float speed = 0.018f;
    public Camera mainCamera;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;

    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    public GameObject recipient;
    public Color defaultColor = Color.white;
    public Color selectedColor = Color.green;
    public Color onMoveColor = Color.cyan;


    // Use this for initialization
    void Start () {
		coll = GetComponent<BoxCollider2D>();
	}

    /* doesn't work from a tutorial 
     * Use the information from the rock for array posstion and which direction
     * depending on the direction add or minus the array position for the new location
     * use a lerp to swap the two tiles
     
    void Update () {
        //Movement attempt
		foreach (Touch touch in Input.touches) {
            Ray ray = mainCamera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, touchInputMask)) {
                GameObject recipient = hit.transform.gameObject;

                if (touch.phase == TouchPhase.Began) {
                    recipient.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
                }
                if (touch.phase == TouchPhase.Ended) {
                    recipient.SendMessage("OnTouchUp", SendMessageOptions.DontRequireReceiver);
                }
                if (touch.phase == TouchPhase.Stationary) {
                    recipient.SendMessage("OnTouchStay", SendMessageOptions.DontRequireReceiver);
                }
                if (touch.phase == TouchPhase.Moved) {
                    recipient.SendMessage("OnTouchMove", SendMessageOptions.DontRequireReceiver);
                }
                if (touch.phase == TouchPhase.Canceled) {
                    recipient.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
	}*/

    void Update() {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            hit = Physics2D.Raycast(touchPos, -Vector2.up, touchInputMask);
            if (hit.collider != null) {
                
                Debug.Log("The hit is: " + hit.collider.name);

                recipient = hit.collider.gameObject;
                recipient.GetComponent<Renderer>().material.color = selectedColor;
            }

            // Handle finger movements based on touch phase.
            switch (touch.phase) {
                // Record initial touch position.
                case TouchPhase.Began:
                    //this.GetComponent<Renderer>().material.color = selectedColor;
                    
                    startPos = touch.position;
                    Debug.Log("The start pos is: " + startPos + " and the recipient is: " + recipient);
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    Debug.Log("The direction is: " + direction);
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen) {
            // Something that uses the chosen direction...
            Debug.Log("YOU GOT HERE!!");
            //coll.GetComponent<Renderer>().material.color = onMoveColor;
        }

    }
}
