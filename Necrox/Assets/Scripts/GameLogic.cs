using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
	
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    private RaycastHit2D directionHit;
    public GameObject gameField;
    private GameObject[,] gameFieldArray;
    public GameObject MatchChecker;
    private bool canTouch = true;
    private bool moving = false;
    private bool moveComplete = false;
    private bool canCheckBoard = false;

    public Vector2 startPos;
    public Vector2 endPos;
    public bool directionChosen = false;
    public string direction;
    private bool gotFirstTouch = false;
    public GameObject firstRock;
    public GameObject secondRock;
    public Color defaultColor = Color.white;
    public Color selectedColor = Color.green;
    public Color onMoveColor = Color.red;
    public int[] rockPos;
    public RaycastHit2D[] raycast2DHits;


    // Use this for initialization
    void Start () {
        gameFieldArray = GameField.GetGameField();
    }

    //Movement
    /*
     * get the touch input
     * get the position in the array
     * use deltaposition to see which direction it went
     * to see which direction find the difference detween the start and stop of both x and y
     * see which number is larger(check if neg or pos)
     * then the difference also tells the direction(up down left right)
     * pass the information to the game logic class with the array posstion and which direction
     */

    void FixedUpdate() {
        //Debug.Log("can touch is: " + canTouch);
        //Debug.Log("moving is: " + moving);
        //Debug.Log("moveComplete is " + moveComplete);

        if (gameField.GetComponent<GameField>().AreVisibleTilesMoving() && !moving) {
            
            canTouch = false;
            moving = true;
        } 
        if (moving) {
            CheckMovementComplete();
        }
        if (Input.GetKeyDown("p")) {
            CheckBoard();
            canTouch = false;
        }
        if (Input.GetKeyDown("l")) {
            canTouch = true;
        }


        if (canTouch) {
            TrackMovement();
        }
        


    }

    void CheckMovementComplete() {
        
        if (moving && !moveComplete ) {
            //Debug.Log("I am here!!!");
            if (!gameField.GetComponent<GameField>().AreVisibleTilesMoving()) {
                // Debug.Log("Hey hey it became false");
                
                CheckBoard();
                
                moving = false;
                
            }
        }
        if (moving && moveComplete) {
            moveComplete = false;
        }
    }

    void CheckBoard() {
        /*for (int x = 0; x < gameFieldArray.GetLength(0); x++) {
            for (int c = 6; c < gameFieldArray.GetLength(1); c++) {
                Debug.Log("This is " + gameFieldArray[x, c] + " in the array");
                if (gameFieldArray[x, c] != null) {
                    gameField.GetComponent<MatchChecker>().BoardCheck(gameFieldArray[x, c]);
                }
                //gameField.GetComponent<MatchChecker>().MatchCheck(gameFieldArray[x, c].gameObject, gameFieldArray[x, c].gameObject);
            }
        }
        canTouch = true;*/
        gameField.GetComponent<MatchChecker>().BoardCheck();
    }

    void TrackMovement() {
        // Track a single touch motion as a direction control.
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            hit = Physics2D.Raycast(touchPos, -Vector2.up, touchInputMask);

            if (hit.collider != null) {
                if (!gotFirstTouch) {
                    //Debug.Log("The hit is: " + hit.collider.name);

                    firstRock = hit.collider.gameObject;
                    //firstRock.GetComponent<Renderer>().material.color = selectedColor;
                    rockPos = firstRock.GetComponent<Rock>().pos;
                    //Debug.Log(rockPos);
                    gotFirstTouch = true;
                }

            }

            // Handle finger movements based on touch phase.
            switch (touch.phase) {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    //Debug.Log("The start pos is: " + startPos + " and the firstRock is: " + firstRock);
                    directionChosen = false;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    endPos = touch.position;
                    //Debug.Log("The direction is: " + endPos);
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen) {
            // if statements to know which direction
            float xDiff = startPos[0] - endPos[0];
            //Debug.Log("xDiff is: " + xDiff);
            if (xDiff < 0) {
                //Debug.Log(xDiff + " xDiff was less than 0");
                xDiff *= -1;
            }
            float yDiff = startPos[1] - endPos[1];
            //Debug.Log("yDiff is: " + yDiff);
            if (yDiff < 0) {
                //Debug.Log(yDiff + " yDiff was less than 0");
                yDiff *= -1;
            }
            //Debug.Log("xDiff is now: " + xDiff + " and the yDiff is: " + yDiff);
            if (xDiff > yDiff) {
                float xResult = startPos[0] - endPos[0];
                if (xResult < 0) {
                    direction = "right";
                    if (firstRock != null) {
                        //directionHit = Physics2D.Raycast(firstRock.transform.position, endPos, touchInputMask);
                        Vector2 endingPos = new Vector2(firstRock.transform.position.x + 50, firstRock.transform.position.y);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
                else {
                    direction = "left";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(firstRock.transform.position.x - 50, firstRock.transform.position.y);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
            }
            else if (xDiff < yDiff) {
                float yResult = startPos[1] - endPos[1];
                if (yResult < 0) {
                    direction = "up";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y + 50);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
                else {
                    direction = "down";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y - 50);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        //Debug.Log("array is: " + raycast2DHits);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
            }
            //Debug.Log(direction);
            if (secondRock != null) {
                gameField.GetComponent<GameField>().MoveTiles(firstRock, secondRock, newMove: true);
            }
            directionChosen = false;
            gotFirstTouch = false;
            firstRock = null;
            secondRock = null;
        }
    }
    public void SetTouchTrue() {
        canTouch = true;
        moveComplete = true;
    }
    //public void SetBoardCheckTrue() {
    //    canCheckBoard = true;
    //}
}
