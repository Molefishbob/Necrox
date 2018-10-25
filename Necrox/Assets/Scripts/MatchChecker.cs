using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour {

    const int POSITIONCHANGE = 25;
    const int MAXAMOUNT = 6;
    public Camera mainCamera;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    public GameObject firstRock;
    public GameObject secondRock;
    private GameObject rockFromHits;
    public RaycastHit2D[] raycast2DHits;
    private Vector2 endingPos;

    public List<GameObject> horizontalMatchList = new List<GameObject>();
    public List<GameObject> verticalMatchList = new List<GameObject>();
    public List<GameObject> horizontalMatchList2 = new List<GameObject>();
    public List<GameObject> verticalMatchList2 = new List<GameObject>();

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
        /*
		if (Input.GetMouseButtonDown(0)) {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            hit = Physics2D.Raycast(mousePos2D, -Vector2.up, touchInputMask);
            //Debug.Log("The mouse click worked and mouse pos is: " + mousePos);
            if (hit.collider != null) {
                firstRock = hit.collider.gameObject;
                //Debug.Log("hit pos is: " + firstRock.transform.position);
                //Debug.Log("You just clicked: " + firstRock + " and element is " + firstRock.GetComponent<Rock>()._element);
                //possibly add a check if the rock is on the sides so doesnt send unnessary rays
                horizontalMatchList.Add(firstRock);
                verticalMatchList.Add(firstRock);
                checkHorizontalRight();
                //checkHorizontalLeft();
                //checkVerticleUp();
                //checkVerticleDown();
            }
        }*/
        if (firstRock != null) {
            if (!firstRock.GetComponent<Rock>().GetMoved()) {
                checkHorizontalRight();
            }
        }
	}

    public void MatchCheck(GameObject tile, GameObject tile2) {
        //Debug.Log("match check called and tile is: " + tile);
        firstRock = tile;
        secondRock = tile2;
        Debug.Log(firstRock.GetComponent<Rock>().GetPos()[0]);
        Debug.Log(secondRock.GetComponent<Rock>().GetPos()[0]);
        horizontalMatchList.Add(firstRock);
        verticalMatchList.Add(firstRock);
        horizontalMatchList2.Add(secondRock);
        verticalMatchList2.Add(secondRock);
    }

    void checkHorizontalRight () {
        //check going right on first rock
        endingPos = new Vector2(firstRock.transform.position.x + POSITIONCHANGE, firstRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);

        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("Going through right loop hits length is " + raycast2DHits.Length);
            //Debug.Log("current i value is: " + i + " and the length is " + raycast2DHits.Length);
            if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        horizontalMatchList.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    } else {
                        i = MAXAMOUNT;
                    }
                }
            }
            
        }
        //Check right on second rock
        endingPos = new Vector2(secondRock.transform.position.x + POSITIONCHANGE, secondRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);

        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("Going through right loop hits length is " + raycast2DHits.Length);
            //Debug.Log("current i value is: " + i + " and the length is " + raycast2DHits.Length);
            if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        horizontalMatchList2.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    }
                    else {
                        i = MAXAMOUNT;
                    }
                }
            }

        }

        CheckHorizontalLeft();
    }

    void CheckHorizontalLeft() {
        //first rock left check
        endingPos = new Vector2(firstRock.transform.position.x - POSITIONCHANGE, firstRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        horizontalMatchList.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    } else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        //second rock left check
        endingPos = new Vector2(secondRock.transform.position.x - POSITIONCHANGE, secondRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        horizontalMatchList2.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    }
                    else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        CheckVerticleUp();
    }
    void CheckVerticleUp() {
        //first rock up check
        endingPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y + POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("arraylength is: " + raycast2DHits.Length + " and i is: " + i);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        verticalMatchList.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    } else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        // second rock up check
        endingPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y + POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("arraylength is: " + raycast2DHits.Length + " and i is: " + i);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        verticalMatchList2.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    }
                    else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        CheckVerticleDown();
    }
    void CheckVerticleDown() {
        //firs rock down check
        endingPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y - POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        verticalMatchList.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    } else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        //second rock down check
        endingPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y - POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++) {
            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits.Length >= 2) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                    //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                    if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                        verticalMatchList2.Add(rockFromHits);
                        //Debug.Log("Counting matches " + i + " : " + secondRock);
                    }
                    else {
                        i = MAXAMOUNT;
                    }
                }
            }
        }
        DestroyMatchesChecker();
    }
    void DestroyMatchesChecker() {
        bool noMatchesHor = false;
        bool noMatchesVer = false;
        bool noMatchesHor2 = false;
        bool noMatchesVer2 = false;
        //detroy the gameobjects if match is 3+
        //First rock check
        if (horizontalMatchList.Count >= 3) {
            for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++) {
                Destroy(horizontalMatchList[mCnt]);
            }

        } else {
            noMatchesHor = true;
        }
        if (verticalMatchList.Count >= 3) {
            for (int mCnt = 0; mCnt < verticalMatchList.Count; mCnt++) {
                Destroy(verticalMatchList[mCnt]);
            }

        } else {
            noMatchesVer = true;
        }
        //seocnd rock check
        if (horizontalMatchList2.Count >= 3) {
            for (int mCnt = 0; mCnt < horizontalMatchList2.Count; mCnt++) {
                Destroy(horizontalMatchList2[mCnt]);
            }

        }
        else {
            noMatchesHor = true;
        }
        if (verticalMatchList2.Count >= 3) {
            for (int mCnt = 0; mCnt < verticalMatchList2.Count; mCnt++) {
                Destroy(verticalMatchList2[mCnt]);
            }

        }
        else {
            noMatchesVer = true;
        }
        if (noMatchesHor && noMatchesVer) {
            RevertPositions();
        }

        /*
         *  if (horizontalMatchList.Count >= 3) {
            for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++) {
                horizontalMatchList[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy:true);
            }

        }
        if (verticalMatchList.Count >= 3) {
            for (int mCnt = 0; mCnt < verticalMatchList.Count; mCnt++) {
                verticalMatchList[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
            }

        }
        foreach (GameObject tile in GameField.GetGameField()) {
            tile.GetComponent<Rock>().DestroyTile();
        }
         */
        //reset the lists
        horizontalMatchList.Clear();
        verticalMatchList.Clear();
    }
    private void RevertPositions() {
        Debug.Log(firstRock.GetComponent<Rock>().GetPos()[0]);
        Debug.Log(secondRock.GetComponent<Rock>().GetPos()[0]);
        gameObject.GetComponent<GameField>().MoveTiles(firstRock,secondRock,newMove: false);
        firstRock = null;
    }
}
