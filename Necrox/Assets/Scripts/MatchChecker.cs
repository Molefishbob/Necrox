using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour {

    const int POSITIONCHANGE = 25;
    const int MAXAMOUNT = 6;
    public Camera mainCamera;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    public GameObject gameLogic;
    public GameObject firstRock;
    public GameObject secondRock;
    public GameObject checkRock;
    private GameObject rockFromHits;
    public RaycastHit2D[] raycast2DHits;
    private Vector2 endingPos;
    private string typeOfCheck;
    private GameObject[,] gameFieldArray;
    private bool isChecking = false;


    public List<GameObject> horizontalMatchList = new List<GameObject>();
    public List<GameObject> verticalMatchList = new List<GameObject>();
    public List<GameObject> horizontalMatchList2 = new List<GameObject>();
    public List<GameObject> verticalMatchList2 = new List<GameObject>();

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
    /*
     * use mouse click to start the raycast chain to test
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
                //Debug.Log("I am HERE");
                if (!isChecking) {
                    checkHorizontalRight();
                }
            }
        }
	}

    public void MatchCheck(GameObject tile, GameObject tile2) {
        //Debug.Log("match check called and tile is: " + tile);
        firstRock = tile;
        secondRock = tile2;
        //Debug.Log(firstRock.GetComponent<Rock>().GetPos()[0]);
        //Debug.Log(secondRock.GetComponent<Rock>().GetPos()[0]);
        horizontalMatchList.Add(firstRock);
        verticalMatchList.Add(firstRock);
        horizontalMatchList2.Add(secondRock);
        verticalMatchList2.Add(secondRock);
    }

    

    void checkHorizontalRight () {
        isChecking = true;
        
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
                        break;
                    }
                }
            }
            
        }
        //Check right on second rock
        if (secondRock != null) {
            endingPos = new Vector2(secondRock.transform.position.x + POSITIONCHANGE, secondRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);

            for (int i = 1; i < raycast2DHits.Length; i++) {
                //Debug.Log("Going through right loop hits length is " + raycast2DHits.Length);
                //Debug.Log("current i value is: " + i + " and the length is " + raycast2DHits.Length);
                if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {
                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                        //Debug.Log("ONE The second rock element is " + secondRock.GetComponent<Rock>()._element);
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                            horizontalMatchList2.Add(rockFromHits);
                            
                            
                        }
                        else {
                            break;
                        }
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
                        break;
                    }
                }
            }
        }
        //second rock left check
        if (secondRock != null) {
            endingPos = new Vector2(secondRock.transform.position.x - POSITIONCHANGE, secondRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
            for (int i = 1; i < raycast2DHits.Length; i++) {
                //Debug.Log("array is: " + raycast2DHits);
                if (raycast2DHits.Length >= 2) {
                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                        //Debug.Log("TWO The second rock element is " + secondRock.GetComponent<Rock>()._element);
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                            horizontalMatchList2.Add(rockFromHits);
                            //Debug.Log("Counting matches " + i + " : " + secondRock);
                        }
                        else {
                            break;
                        }
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
                        break;
                    }
                }
            }
        }
        // second rock up check
        if (secondRock != null) {
            endingPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y + POSITIONCHANGE);
            raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
            for (int i = 1; i < raycast2DHits.Length; i++) {
                //Debug.Log("arraylength is: " + raycast2DHits.Length + " and i is: " + i);
                if (raycast2DHits.Length >= 2) {
                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                        //Debug.Log("THREE The second rock element is " + secondRock.GetComponent<Rock>()._element);
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                            if (rockFromHits.GetComponent<Rock>()._element == null) {
                                Debug.Log("WHY ARE YOU HERE YOU ARE NULLLLLLL" + rockFromHits.GetComponent<Rock>()._element);
                            }
                            verticalMatchList2.Add(rockFromHits);
                            //Debug.Log("Counting matches " + i + " : " + secondRock);
                        }
                        else {
                            break;
                        }
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
                        break;
                    }
                }
            }
        }
        //second rock down check
        if (secondRock != null) {
            endingPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y - POSITIONCHANGE);
            raycast2DHits = Physics2D.RaycastAll(secondRock.transform.position, endingPos, touchInputMask);
            for (int i = 1; i < raycast2DHits.Length; i++) {
                //Debug.Log(" is: " + raycast2DHits);
                if (raycast2DHits.Length >= 2) {
                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                        //Debug.Log("FOUR The second rock element is " + secondRock.GetComponent<Rock>()._element);
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                            verticalMatchList2.Add(rockFromHits);
                            //Debug.Log("Counting matches " + i + " : " + secondRock);
                        }
                        else {
                            break;
                        }
                    }
                }
            }
        }
        DestroyMatchesChecker();

    }
    void DestroyMatchesChecker() {
        Debug.Log("first hor ma length is now " + horizontalMatchList.Count);
        Debug.Log("first ver ma length is now " + verticalMatchList.Count);
        Debug.Log("second hor ma length is now " + horizontalMatchList2.Count);
        Debug.Log("second ver ma length is now " + verticalMatchList2.Count);
        bool noMatchesHor = false;
        bool noMatchesVer = false;
        bool noMatchesHor2 = false;
        bool noMatchesVer2 = false;
        
        if (horizontalMatchList.Count >= 3)
        {
            for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++)
            {
                horizontalMatchList[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
            }
        }
        else
        {
            noMatchesHor = true;
        }
        if (verticalMatchList.Count >= 3)
        {
            for (int mCnt = 0; mCnt < verticalMatchList.Count; mCnt++)
            {   
                verticalMatchList[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
            }
        }
        else
        {
            noMatchesVer = true;
        }
        if (horizontalMatchList2.Count >= 3)
        {
            for (int mCnt = 0; mCnt < horizontalMatchList2.Count; mCnt++)
            {
                horizontalMatchList2[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
            }
        }
        else
        {
            noMatchesHor2 = true;
        }
        if (verticalMatchList2.Count >= 3)
        {
            for (int mCnt = 0; mCnt < verticalMatchList2.Count; mCnt++)
            {
                verticalMatchList2[mCnt].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
            }
        }
        else
        {
            noMatchesVer2 = true;
        }
        if (noMatchesHor && noMatchesVer && noMatchesHor2 && noMatchesVer2)
        {
            RevertPositions();
        }
        for (int a = 0; a < GameField.GetGameField().GetLength(0); a++)
        {
            for (int b = 6; b < GameField.GetGameField().GetLength(1); b++)
            {
                GameField.GetGameField()[a, b].GetComponent<Rock>().DestroyTile();
            }
        }
        ResetMatchChecker();

    }

    private void ResetMatchChecker() {
        firstRock = null;
        secondRock = null;
        checkRock = null;
        isChecking = false;
        horizontalMatchList.Clear();
        verticalMatchList.Clear();
        horizontalMatchList2.Clear();
        verticalMatchList2.Clear();
    }

    private void RevertPositions() {
        //Debug.Log(firstRock.GetComponent<Rock>().GetPos()[0]);
        //Debug.Log(secondRock.GetComponent<Rock>().GetPos()[0]);
        gameObject.GetComponent<GameField>().MoveTiles(firstRock,secondRock,newMove: false);
        ResetMatchChecker();
    }
    //do the same check but iterate through the array
    public void BoardCheck() {
        gameFieldArray = GameField.GetGameField();
        //Debug.Log(gameFieldArray);

        for (int x = 0; x < gameFieldArray.GetLength(0); x++) {
            for (int c = 6; c < gameFieldArray.GetLength(1); c++) {
                //Debug.Log("This is " + gameFieldArray[x, c] + " in the array");
                if (gameFieldArray[x, c] != null) {
                    checkRock = gameFieldArray[x, c];
                    horizontalMatchList.Add(checkRock);
                    verticalMatchList.Add(checkRock);
                    //check right
                    endingPos = new Vector2(checkRock.transform.position.x - POSITIONCHANGE, checkRock.transform.position.y);
                    raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
                    for (int i = 1; i < raycast2DHits.Length; i++) {
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits.Length >= 2) {
                            if (raycast2DHits[i].collider != null) {

                                rockFromHits = raycast2DHits[i].collider.gameObject;
                                //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                                //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                                if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                                    horizontalMatchList.Add(rockFromHits);
                                    //Debug.Log("Counting matches " + i + " : " + secondRock);
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                    //check left
                    endingPos = new Vector2(checkRock.transform.position.x + POSITIONCHANGE, checkRock.transform.position.y);
                    raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
                    for (int i = 1; i < raycast2DHits.Length; i++) {
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits.Length >= 2) {
                            if (raycast2DHits[i].collider != null) {

                                rockFromHits = raycast2DHits[i].collider.gameObject;
                                //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                                //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                                if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                                    horizontalMatchList.Add(rockFromHits);
                                    //Debug.Log("Counting matches " + i + " : " + secondRock);
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                    //check up
                    endingPos = new Vector2(checkRock.transform.position.x, checkRock.transform.position.y + POSITIONCHANGE);
                    raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
                    for (int i = 1; i < raycast2DHits.Length; i++) {
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits.Length >= 2) {
                            if (raycast2DHits[i].collider != null) {

                                rockFromHits = raycast2DHits[i].collider.gameObject;
                                //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                                //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                                if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                                    verticalMatchList.Add(rockFromHits);
                                    //Debug.Log("Counting matches " + i + " : " + secondRock);
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                    //check down
                    endingPos = new Vector2(checkRock.transform.position.x, checkRock.transform.position.y - POSITIONCHANGE);
                    raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
                    for (int i = 1; i < raycast2DHits.Length; i++) {
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits.Length >= 2) {
                            if (raycast2DHits[i].collider != null) {

                                rockFromHits = raycast2DHits[i].collider.gameObject;
                                //Debug.Log("First rock is: " + firstRock + " and the rock to the right is: " + secondRock);
                                //Debug.Log("The first rock element is " + firstRock.GetComponent<Rock>()._element);
                                if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {
                                    verticalMatchList.Add(rockFromHits);
                                    //Debug.Log("Counting matches " + i + " : " + secondRock);
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                    //destroy if there is matches
                    // Debug.Log("first hor ma length is " + horizontalMatchList.Count);
                    // Debug.Log("first ver ma length is " + verticalMatchList.Count);
                    for (int k = 0; k < horizontalMatchList.Count; k++) {
                        if (horizontalMatchList[k] == null) {
                            Debug.Log("There was a null");
                        }
                    }
                    /*horizontalMatchList.RemoveAll(null);
                    horizontalMatchList2.RemoveAll(null);
                    verticalMatchList.RemoveAll(null);
                    verticalMatchList2.RemoveAll(null);
                    for (int k = 0; k < horizontalMatchList2.Count; k++) {
                        Debug.Log("Tile " + k + " is: " + horizontalMatchList2[k]);
                        if (horizontalMatchList2[k] == null) {
                            Debug.Log("There was a null");
                        }
                    }
                    Debug.Log("first hor ma length is now " + horizontalMatchList.Count);
                    Debug.Log("first ver ma length is now " + verticalMatchList.Count);
                    Debug.Log("second hor ma length is now " + horizontalMatchList2.Count);
                    Debug.Log("second ver ma length is now " + verticalMatchList2.Count);*/
                    if (horizontalMatchList.Count >= 3) {
                        for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++) {
                            Destroy(horizontalMatchList[mCnt]);
                        }

                    }
                    if (verticalMatchList.Count >= 3) {
                        for (int mCnt = 0; mCnt < verticalMatchList.Count; mCnt++) {
                            Destroy(verticalMatchList[mCnt]);
                        }

                    }
                    //reset the lists and rocks
                    ResetMatchChecker();
                }
            }
        }
        Debug.Log("dasfdasfadfafa");
        gameLogic.GetComponent<GameLogic>().SetTouchTrue();

    }
}