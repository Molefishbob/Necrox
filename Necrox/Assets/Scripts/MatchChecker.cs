using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour {

    public Camera mainCamera;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    public GameObject firstRock;
    public GameObject secondRock;
    public RaycastHit2D[] raycast2DHits;
    private Vector2 endingPos;

    public List<GameObject> horizontalMatchList = new List<GameObject>();
    public List<GameObject> verticalMatchList = new List<GameObject>();

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
    /*
     * use mouse click to start the raycast  chain
     * get the gameobject from the raycast
     * send raycast in all directions
     * if the neighboring gameobject is the same as the first cast another ray in the same direction
     * continue until it is different
     * ïf the array is greater than or equal to 3 destroy the game objects in the array
     */
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            hit = Physics2D.Raycast(mousePos2D, -Vector2.up, touchInputMask);
            //Debug.Log("The mouse click worked and mouse pos is: " + mousePos + "hit is: " + hit.collider);
            if (hit.collider != null) {
                firstRock = hit.collider.gameObject;
                Debug.Log("You just clicked: " + firstRock + " and element is " + firstRock.GetComponent<Rock>()._element);
                //possibly add a check if the rock is on the sides so doesnt send unnessary rays
                horizontalMatchList.Add(firstRock);
                verticalMatchList.Add(firstRock);
                checkHorizontalRight();
                //checkHorizontalLeft();
                //checkVerticleUp();
                //checkVerticleDown();
            }
        }
	}

    void checkHorizontalRight () {
        //check going right
        for (int i = 1; i < 6; i++) {
            endingPos = new Vector2(firstRock.transform.position.x + 150, firstRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits[i].collider != null) {

                secondRock = raycast2DHits[i].collider.gameObject;
                //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                if (firstRock.GetComponent<Rock>()._element == secondRock.GetComponent<Rock>()._element) {
                    horizontalMatchList.Add(secondRock);
                    //Debug.Log("Counting matches " + i + " : " + secondRock);
                } else {
                    i = 6;
                }
            }
        }
        //test destroying the gameobjects if match is 3+
        if (horizontalMatchList.Count >= 3) {
            for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++) {
                Destroy(horizontalMatchList[mCnt]);
            }
            
        }
        horizontalMatchList.Clear();
    }

    void checkHorizontalLeft() {

    }
    void checkVerticleUp() {

    }
    void checkVerticleDown() {

    }
}
