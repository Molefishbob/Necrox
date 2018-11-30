using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    private const int MINDISTANCE = 15;
    private const int Offset = 6;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    private RaycastHit2D directionHit;
    public GameObject gameField;
    public GameObject MatchChecker;
    private bool canTouch = true;
    private bool moving = false;
    private bool moveComplete = false;

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
    public RaycastHit2D[] raycast2DHits;

    public GameObject selectBorder;
    private GameObject border;
    private bool _touched;
    private int _counter = 0;
    
    public bool _paused {
        get;
        set;
    }

    
    // Use this for initialization
    void Start () {

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
        if (!_paused) {
            //Debug.Log("can touch is: " + canTouch);
            //Debug.Log("moving is: " + moving);
            //Debug.Log("moveComplete is " + moveComplete);
            if(!gameField.GetComponent<GameField>().AreVisibleTilesMoving()) {
                canTouch = true;
            }

            if (gameField.GetComponent<GameField>().AreVisibleTilesMoving() && !moving) {
                
                canTouch = false;
            }
            if (moving) {
                CheckMovementComplete();
            }
            if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape)) {
                CheckBoard();
                canTouch = false;
            }
            if (Input.GetKeyDown("l")) {
                canTouch = true;
            }


            if (canTouch && !gameField.GetComponent<GameField>().AreVisibleTilesMoving()) {
                TrackMovement();
            }
            
            if(!gameField.GetComponent<GameField>().AreVisibleTilesMoving()) {
                if (_counter >= 15) {
                    CheckBoard();
                    _counter = 0;
                }
                _counter++;
            }
        }
    }

    void CheckMovementComplete() {
        
        if (!moveComplete) {
            // Debug.Log("Moving");
            // Debug.Log("Tiles moving:" + gameField.GetComponent<GameField>().AreVisibleTilesMoving());
            if (!gameField.GetComponent<GameField>().AreVisibleTilesMoving()) {
                
                CheckBoard();
                
            }
        }
        if (moveComplete) {
            moveComplete = false;
            moving = false;
        }
    }

    void CheckBoard() {
        gameField.GetComponent<MatchChecker>().BoardCheck();
    }
    public void SetSwipeChecking(bool value) {
        moving = false;
    }

    void TrackMovement() {
        // Track a single touch motion as a direction control.
        if (Input.touchCount == 1) {

            canTouch = false;
            Touch touch = Input.GetTouch(0);
            Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            hit = Physics2D.Raycast(touchPos, -Vector2.up, touchInputMask);

            if (hit.collider != null) {

                if (!gotFirstTouch) {
                    //Debug.Log("The hit is: " + hit.collider.name);

                        firstRock = hit.collider.gameObject;
                        if (!firstRock.GetComponent<Rock>().sentToFeedback
                            && !firstRock.GetComponent<Rock>().isImmovable
                            && !firstRock.GetComponentInParent<ColumnBehaviour>().tileSwitchDisabled) {

                        //firstRock.GetComponent<Renderer>().material.color = selectedColor;
                        border = Instantiate(selectBorder, new Vector3(firstRock.transform.position.x, firstRock.transform.position.y, 0), Quaternion.identity);
                        border.transform.parent = firstRock.transform;
                        gotFirstTouch = true;

                    }
                }

            }

            // Handle finger movements based on touch phase.
            switch (touch.phase) {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    Debug.Log(startPos);
                    directionChosen = false;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    endPos = touch.position;
                    Debug.Log(endPos);
                    Destroy(border);
                    float xDiff = startPos[0] - endPos[0];
                    if (xDiff < 0) {
                        xDiff *= -1;
                    }
                    float yDiff = startPos[1] - endPos[1];
                    if (yDiff < 0) {
                        yDiff *= -1;
                    }
                    if (yDiff >= MINDISTANCE || xDiff >= MINDISTANCE) {
                        directionChosen = true;
                    } else {
                        gotFirstTouch = false;
                    }
                    
                    break;
            }
        }
        if (directionChosen && gotFirstTouch) {
            // if statements to know which direction
            float xDiff = startPos[0] - endPos[0];
            if (xDiff < 0) {
                xDiff *= -1;
            }
            float yDiff = startPos[1] - endPos[1];
            if (yDiff < 0) {
                yDiff *= -1;
            }
            if (xDiff > yDiff) {
                float xResult = startPos[0] - endPos[0];
                if (xResult < 0) {
                    direction = "right";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(Offset, 0);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        Debug.DrawRay(firstRock.transform.position,endingPos,Color.magenta,5f);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
                else {
                    direction = "left";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(-Offset, 0);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        Debug.DrawRay(firstRock.transform.position,endingPos,Color.magenta,5f);
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
                        Vector2 endingPos = new Vector2(0, Offset);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        Debug.DrawRay(firstRock.transform.position,endingPos,Color.magenta,5f);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
                else {
                    direction = "down";
                    if (firstRock != null) {
                        Vector2 endingPos = new Vector2(0,-Offset);
                        raycast2DHits = Physics2D.RaycastAll(firstRock.transform.position, endingPos, touchInputMask);
                        Debug.DrawRay(firstRock.transform.position,endingPos,Color.magenta,5f);
                        if (raycast2DHits[1].collider != null) {
                            secondRock = raycast2DHits[1].collider.gameObject;
                            //secondRock.GetComponent<Renderer>().material.color = onMoveColor;
                        }
                    }
                }
            }
            //Debug.Log(direction);
            if (secondRock != null && !secondRock.GetComponent<Rock>().sentToFeedback
                && !secondRock.GetComponent<Rock>().GetMoved()
                && !secondRock.GetComponent<Rock>().isImmovable
                && !secondRock.GetComponentInParent<ColumnBehaviour>().tileSwitchDisabled) {

                //Debug.Log("Element:" + secondRock.GetComponent<Rock>().GetElement());
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
    public void SetTouchFalse() {
        canTouch = false;
    }
}
