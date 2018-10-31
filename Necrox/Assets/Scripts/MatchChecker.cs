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
    private bool noMatchesHor = false;
    private bool noMatchesVer = false;
    private bool noMatchesHor2 = false;
    private bool noMatchesVer2 = false;


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
	void FixedUpdate () {
       
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
        firstRock = tile;
        secondRock = tile2;

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

            if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) 
            {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                        horizontalMatchList.Add(rockFromHits);

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

                if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

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

            if (raycast2DHits.Length >= 2) {

                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                        horizontalMatchList.Add(rockFromHits);

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

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

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
        CheckVerticleUp();
    }
    void CheckVerticleUp() {
        //first rock up check
        endingPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y + POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);

        for (int i = 1; i < raycast2DHits.Length; i++) {

            if (raycast2DHits.Length >= 2) {

                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                        verticalMatchList.Add(rockFromHits);

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

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                            if (rockFromHits.GetComponent<Rock>()._element == null) {
                                
                            }

                            verticalMatchList2.Add(rockFromHits);
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
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                        verticalMatchList.Add(rockFromHits);

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

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element) {

                            verticalMatchList2.Add(rockFromHits);

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
    void DestroyMatchesChecker()
    {

        CheckVerticalMatches();

        CheckHorizontalMatches();

        if (noMatchesHor && noMatchesVer && noMatchesHor2 && noMatchesVer2)
        {
            RevertPositions();
        }

        OrderTilesToBeDestroyed();

        ResetMatchChecker();

    }

    private static void OrderTilesToBeDestroyed()
    {
        for (int a = 0; a < GameField.GetGameField().GetLength(0); a++)
        {
            for (int b = 6; b < GameField.GetGameField().GetLength(1); b++)
            {
                GameField.GetGameField()[a, b].GetComponent<Rock>().DestroyTile();
            }
        }
    }

    private void CheckHorizontalMatches()
    {
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
    }

    private void CheckVerticalMatches()
    {
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
        // Add boolean that enables the boardcheck in GameLogic
    }

    private void RevertPositions() {

        gameObject.GetComponent<GameField>().MoveTiles(firstRock,secondRock,newMove: false);
        ResetMatchChecker();

    }

    //do the same check but iterate through the array
    public void BoardCheck() {
        gameFieldArray = GameField.GetGameField();

        for (int x = 0; x < gameFieldArray.GetLength(0); x++) {

            for (int c = 6; c < gameFieldArray.GetLength(1); c++) {
                
                if (gameFieldArray[x, c] != null)
                {

                    checkRock = gameFieldArray[x, c];
                    horizontalMatchList.Add(checkRock);
                    verticalMatchList.Add(checkRock);

                    //check right
                    RightCheck();

                    //check left
                    LeftCheck();

                    //check up
                    UpCheck();

                    //check down
                    DownCheck();

                    //destroy if there is matches
                    DestroyMatches();

                    //reset the lists and rocks
                    ResetMatchChecker();

                }
            }
        }
        gameLogic.GetComponent<GameLogic>().SetTouchTrue();

    }

    private void DestroyMatches()
    {
        for (int k = 0; k < horizontalMatchList.Count; k++)
        {
            if (horizontalMatchList[k] == null)
            {
                Debug.Log("There was a null");
            }
        }

        if (horizontalMatchList.Count >= 3)
        {
            for (int mCnt = 0; mCnt < horizontalMatchList.Count; mCnt++)
            {
                Destroy(horizontalMatchList[mCnt]);
            }

        }
        if (verticalMatchList.Count >= 3)
        {
            for (int mCnt = 0; mCnt < verticalMatchList.Count; mCnt++)
            {
                Destroy(verticalMatchList[mCnt]);
            }

        }
    }

    private void DownCheck()
    {
        endingPos = new Vector2(checkRock.transform.position.x, checkRock.transform.position.y - POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);

        for (int i = 1; i < raycast2DHits.Length; i++)
        {

            if (raycast2DHits.Length >= 2)
            {
                if (raycast2DHits[i].collider != null)
                {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element)
                    {
                        verticalMatchList.Add(rockFromHits);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    private void UpCheck()
    {
        endingPos = new Vector2(checkRock.transform.position.x, checkRock.transform.position.y + POSITIONCHANGE);
        raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++)
        {

            if (raycast2DHits.Length >= 2)
            {
                if (raycast2DHits[i].collider != null)
                {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element)
                    {
                        verticalMatchList.Add(rockFromHits);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    private void LeftCheck()
    {
        endingPos = new Vector2(checkRock.transform.position.x + POSITIONCHANGE, checkRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);
        for (int i = 1; i < raycast2DHits.Length; i++)
        {
            if (raycast2DHits.Length >= 2)
            {
                if (raycast2DHits[i].collider != null)
                {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element)
                    {
                        horizontalMatchList.Add(rockFromHits);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    private void RightCheck()
    {
        endingPos = new Vector2(checkRock.transform.position.x - POSITIONCHANGE, checkRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(checkRock.transform.position, endingPos, touchInputMask);

        for (int i = 1; i < raycast2DHits.Length; i++)
        {

            if (raycast2DHits.Length >= 2)
            {
                if (raycast2DHits[i].collider != null)
                {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (checkRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element)
                    {
                        horizontalMatchList.Add(rockFromHits);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}