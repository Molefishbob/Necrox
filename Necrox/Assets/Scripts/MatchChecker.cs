using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour {

    const int POSITIONCHANGE = 6;
    const int MAXAMOUNT = 6;
    private const string Debris = "debris";
    public Camera mainCamera;
    public LayerMask touchInputMask;
    private RaycastHit2D hit;
    public GameObject gameLogic;
    private bool swipeCheck;
    public GameObject firstRock;
    public GameObject secondRock;
    public GameObject checkRock;
    private GameObject rockFromHits;
    public RaycastHit2D[] raycast2DHits;
    [Range(1,6)]
    public int _minimumMatch;
    public GameObject _resetText;

    private Vector2 endingPos;
    private Vector2 startPos;
    private string typeOfCheck;
    private GameObject[,] gameFieldArray;
    private bool isChecking = false;
    private bool noMatchesHor = false;
    private bool noMatchesVer = false;
    private bool noMatchesHor2 = false;
    private bool noMatchesVer2 = false;
    private int _fieldColumns;
    private int _fieldRows;
    private int _fieldVisibleRows;
    private string _firstRockElement;
    private Timer _timer;
    private bool _waitingTimer;

    public List<GameObject> horizontalMatchList = new List<GameObject>();
    public List<GameObject> verticalMatchList = new List<GameObject>();
    public List<GameObject> horizontalMatchList2 = new List<GameObject>();
    public List<GameObject> verticalMatchList2 = new List<GameObject>();
    

    // Use this for initialization
    void Start() {
        _fieldColumns = GameField.GetGameField().GetLength(0);
        _fieldRows = GameField.GetGameField().GetLength(1);
        _fieldVisibleRows = GameField.GetGameField().GetLength(1)/2;
        _timer = GetComponent<Timer>();
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
    void FixedUpdate() {

        if (_waitingTimer)
        {
            if (_timer.IsCompleted)
            {
                _resetText.gameObject.SetActive(false);
                GetComponent<GameField>().CreateNewArrays(destroyOldOnes: true);
                _waitingTimer = false;

            }

        }

        if (firstRock != null) {
            if (!firstRock.GetComponent<Rock>().GetMoved()) {
                if (!isChecking) {
                    checkHorizontalRight();
                }
            }
        }
    }

    public void MatchCheck(GameObject tile, GameObject tile2) {
        gameLogic.GetComponent<GameLogic>().SetSwipeChecking(true);
        firstRock = tile;
        secondRock = tile2;
        //Debug.Log("match check has been called and first rock is: " + firstRock);
        horizontalMatchList.Add(firstRock);
        verticalMatchList.Add(firstRock);

        horizontalMatchList2.Add(secondRock);
        verticalMatchList2.Add(secondRock);
    }


    #region DirectionCheck
    void checkHorizontalRight() {
        //Debug.Log("Checking right");
        isChecking = true;

        //check going right on first rock
        endingPos = new Vector2(POSITIONCHANGE,0);
        startPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
        Debug.DrawRay(startPos, endingPos,Color.red,5f);

        for (int i = 1; i < raycast2DHits.Length; i++) {

            if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {
                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {
                        
                        horizontalMatchList.Add(rockFromHits);

                    } else {
                        break;
                    }
                }
            }

        }
        //Check right on second rock
        if (secondRock != null) {

            endingPos = new Vector2(POSITIONCHANGE,0);
            startPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
            Debug.DrawRay(startPos, endingPos,Color.red,5f);

            for (int i = 1; i < raycast2DHits.Length; i++) {

                if (raycast2DHits.Length >= 2 || i <= raycast2DHits.Length) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

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
        startPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y);
        endingPos = new Vector2(-POSITIONCHANGE, 0);
        raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
        Debug.DrawRay(startPos, endingPos,Color.blue,5f);

        for (int i = 1; i < raycast2DHits.Length; i++) {

            if (raycast2DHits.Length >= 2) {

                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {
                        
                        horizontalMatchList.Add(rockFromHits);

                    } else {
                        break;
                    }
                }
            }
        }
        //second rock left check
        if (secondRock != null) {

            startPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y);
            endingPos = new Vector2(-POSITIONCHANGE, 0);
            raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
            Debug.DrawRay(startPos, endingPos,Color.blue,5f);

            for (int i = 1; i < raycast2DHits.Length; i++) {

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

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
        endingPos = new Vector2(0,POSITIONCHANGE);
        startPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
        Debug.DrawRay(startPos, endingPos,Color.green,5f);

        for (int i = 1; i < raycast2DHits.Length; i++) {

            if (raycast2DHits.Length >= 2) {

                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;

                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

                        verticalMatchList.Add(rockFromHits);

                    } else {
                        break;
                    }
                }
            }
        }
        // second rock up check
        if (secondRock != null) {

            endingPos = new Vector2(0,POSITIONCHANGE);
            startPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
            Debug.DrawRay(startPos, endingPos,Color.green,5f);

            for (int i = 1; i < raycast2DHits.Length; i++) {

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;
                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

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
        endingPos = new Vector2(0,-POSITIONCHANGE);
        startPos = new Vector2(firstRock.transform.position.x, firstRock.transform.position.y);
        raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
        Debug.DrawRay(startPos, endingPos,Color.yellow,5f);

        for (int i = 1; i < raycast2DHits.Length; i++) {

            //Debug.Log("array is: " + raycast2DHits);
            if (raycast2DHits.Length >= 2) {

                if (raycast2DHits[i].collider != null) {

                    rockFromHits = raycast2DHits[i].collider.gameObject;
                    if (firstRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

                        verticalMatchList.Add(rockFromHits);

                    } else {
                        break;
                    }
                }
            }
        }
        //second rock down check
        if (secondRock != null) {

            endingPos = new Vector2(0,-POSITIONCHANGE);
            startPos = new Vector2(secondRock.transform.position.x, secondRock.transform.position.y);
            raycast2DHits = Physics2D.RaycastAll(startPos, endingPos, touchInputMask);
            Debug.DrawRay(startPos, endingPos,Color.yellow,5f);

            for (int i = 1; i < raycast2DHits.Length; i++) {

                if (raycast2DHits.Length >= 2) {

                    if (raycast2DHits[i].collider != null) {

                        rockFromHits = raycast2DHits[i].collider.gameObject;

                        if (secondRock.GetComponent<Rock>()._element == rockFromHits.GetComponent<Rock>()._element && !rockFromHits.GetComponent<Rock>().sentToFeedback) {

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
    #endregion DirectionCheck
    void DestroyMatchesChecker() {

        CheckVerticalMatches();

        CheckHorizontalMatches();
        if (noMatchesHor && noMatchesVer && noMatchesHor2 && noMatchesVer2) {
            RevertPositions();
        } else {
            OrderTilesToBeDestroyed();
        }

        gameLogic.GetComponent<GameLogic>().SetSwipeChecking(false);

        ResetMatchChecker();

    }

    private static bool OrderTilesToBeDestroyed() {
        bool destroyed = false;
        for (int a = 0; a < GameField.GetGameField().GetLength(0); a++) {
            for (int b = 6; b < GameField.GetGameField().GetLength(1); b++) {
                if (GameField.GetGameField()[a, b] != null) {
                    if (GameField.GetGameField()[a, b].GetComponent<Rock>()._toBeDestroyed) {
                        destroyed = true;
                    }
                    GameField.GetGameField()[a, b].GetComponent<Rock>().DestroyTile();
                }
            }
        }
        return destroyed;
    }

    private void CheckHorizontalMatches() { 
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

    private void CheckVerticalMatches() {
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
        //Debug.Log("Reset");
        firstRock = null;
        secondRock = null;
        checkRock = null;
        isChecking = false;
        horizontalMatchList.Clear();
        verticalMatchList.Clear();
        horizontalMatchList2.Clear();
        verticalMatchList2.Clear();
        noMatchesHor = false;
        noMatchesVer = false;
        noMatchesHor2 = false;
        noMatchesVer2 = false;
    }

    private void RevertPositions() {
        if(firstRock.transform.childCount > 0) {
            Destroy(firstRock.transform.GetChild(0).gameObject);
        }
        if(secondRock.transform.childCount > 0) {
            Destroy(secondRock.transform.GetChild(0).gameObject);
        }
        gameObject.GetComponent<GameField>().MoveTiles(firstRock,secondRock,newMove: false);
        ResetMatchChecker();

    }

    //do the same check but iterate through the array
    public void BoardCheck() {
        if (CheckForPotentialMatches())
        {
            ColumnCheck();

            RowCheck();

            if (OrderTilesToBeDestroyed())
            {
                gameLogic.GetComponent<GameLogic>()._noMatches = false;
            }
            else
            {
                gameLogic.GetComponent<GameLogic>()._noMatches = true;
            }

            gameLogic.GetComponent<GameLogic>().SetTouchTrue();

            ResetMatchChecker();
        }
    }

    public void ColumnCheck() {
        bool noMatch;

        for(int a = 0; a < _fieldColumns; a++) {

            _firstRockElement = GameField.GetGameField()[a,6].GetComponent<Rock>().GetElement();
            
            int counter = 1;

            for (int b = _fieldVisibleRows + 1; b < _fieldRows; b++) {
                noMatch = true;
                Rock tile = GameField.GetGameField()[a,b].GetComponent<Rock>();

                if (tile.GetElement() == _firstRockElement 
                    && !tile.sentToFeedback 
                    && (tile.GetElement() != Debris 
                    || _firstRockElement != Debris)) {

                    noMatch = false;
                    counter++;

                    if (b+1 ==  _fieldRows && counter >= _minimumMatch) {

                        for (int c = 0; counter > 0; c++) {
                            
                            GameField.GetGameField()[a,b-c].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
                            counter--;

                        }
                        counter = 1;
                    }

                }
                if (counter >= _minimumMatch && noMatch) {

                    _firstRockElement = tile.GetElement();

                    for (int c = 1; counter > 0; c++) {
                        
                        GameField.GetGameField()[a,b-c].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
                        counter--;

                    }
                    counter = 1;

                } else if (noMatch) {
                    counter = 1;
                }
                _firstRockElement = tile.GetElement();
            }
        }
    }

    public void RowCheck() {
        bool noMatch;

        for(int b = _fieldVisibleRows; b < _fieldRows; b++) {

            _firstRockElement = GameField.GetGameField()[0,b].GetComponent<Rock>().GetElement();
            int counter = 1;

            for (int a = 1; a < _fieldColumns; a++) {

                noMatch = true;
                Rock tile = GameField.GetGameField()[a,b].GetComponent<Rock>();

                if (tile.GetElement() == _firstRockElement 
                    && !tile.sentToFeedback 
                    && (tile.GetElement() != Debris 
                    || _firstRockElement != Debris)) {

                    noMatch = false;
                    counter++;

                    if (a+1 ==  _fieldColumns && counter >= _minimumMatch) {

                        for (int c = 0; counter > 0; c++) {

                            GameField.GetGameField()[a-c,b].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
                            counter--;

                        }
                        counter = 1;
                    }
                }

                if (counter >= _minimumMatch && noMatch) {

                    _firstRockElement = tile.GetElement();

                    for (int c = 1; counter > 0; c++) {

                        GameField.GetGameField()[a-c,b].GetComponent<Rock>().SetToBeDestroyed(destroy: true);
                        counter--;

                    }
                    counter = 1;

                } else if (noMatch) {
                    counter = 1;
                }
                _firstRockElement = tile.GetElement();
            }
        }
    }

    public bool CheckForPotentialMatches()
    {
        if (!_waitingTimer && !GetComponent<GameField>()._creatingNewTiles)
        {
            bool columns = PotentialColumnMatches();
            bool rows = PotentialRowMatches();

            if (!columns && !rows)
            {
                FindObjectOfType<GameLogic>()._paused = true;
                FindObjectOfType<CombatUI>()._paused = true;
                _resetText.gameObject.SetActive(true);
                ResetTimer();
                _waitingTimer = true;
                return true;
            }
        }
        return false;
    }

    private bool PotentialColumnMatches()
    {
        bool potentialMatch = false;

        for (int a = 0; a < _fieldColumns; a++)
        {
            
            _firstRockElement = GameField.GetGameField()[a, 6].GetComponent<Rock>().GetElement();

            for (int b = _fieldVisibleRows +1; b < _fieldRows; b++)
            {

                if (ColumnUnderCheck(potentialMatch, a, b))
                {
                    return true;
                }
                if (ColumnOverCheck(potentialMatch, a, b - 2))
                {
                    return true;
                }
                _firstRockElement = GameField.GetGameField()[a, b].GetComponent<Rock>().GetElement();

            }

        }
        return false;

    }

    private bool ColumnOverCheck(bool potentialMatch, int a, int b)
    {
        Rock tile;
        if (b >= 6)
        {
            
            tile = GameField.GetGameField()[a, b].GetComponent<Rock>();
            if (tile.GetElement() == _firstRockElement
                && !tile.sentToFeedback
                && (tile.GetElement() != Debris
                || _firstRockElement != Debris))
            {
                if (a + 1 != 6 && b - 1 >= 6)
                {
                    Rock temp = GameField.GetGameField()[a + 1, b - 1].GetComponent<Rock>();

                    if (tile.GetElement() == temp.GetElement() 
                        && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {
                        
                        return true;

                    }

                    if (a - 1 >= 0)
                    {

                        temp = GameField.GetGameField()[a - 1, b - 1].GetComponent<Rock>();
                        if (tile.GetElement() == temp.GetElement() 
                            && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }

                    }
                    if (b - 2 >= 6 && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris)
                    {
                        temp = GameField.GetGameField()[a, b - 2].GetComponent<Rock>();
                        if (tile.GetElement() == temp.GetElement() 
                            && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }

                    }

                }
                if (b + 1 != _fieldRows)
                {
                    tile = GameField.GetGameField()[a, b + 1].GetComponent<Rock>();
                    if (tile.GetElement() == _firstRockElement)
                    {
                        if (a - 1 >= 0)
                        {
                            Rock temp = GameField.GetGameField()[a - 1, b].GetComponent<Rock>();
                            if (temp.GetElement() == tile.GetElement()
                                && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris
                                && temp.GetElement() != Debris
                                && tile.GetElement() != Debris)
                            {

                                return true;

                            }
                        }
                        if (a + 1 != 6)
                        {
                            Rock temp = GameField.GetGameField()[a + 1, b].GetComponent<Rock>();
                            if (temp.GetElement() == tile.GetElement()
                                && GameField.GetGameField()[a, b - 1].GetComponent<Rock>().GetElement() != Debris
                                && temp.GetElement() != Debris
                                & tile.GetElement() != Debris)
                            {

                                return true;

                            }
                        }
                    }
                }

            }
            
        }
        return false;
    }

    private bool ColumnUnderCheck(bool potentialMatch, int a, int b)
    {
        Rock tile = GameField.GetGameField()[a, b].GetComponent<Rock>();

        if (tile.GetElement() == _firstRockElement
            && !tile.sentToFeedback
            && (tile.GetElement() != Debris
            || _firstRockElement != Debris))
        {
            if (a + 1 != 6 && b + 1 != _fieldRows)
            {
                Rock temp = GameField.GetGameField()[a + 1, b + 1].GetComponent<Rock>();

                if (tile.GetElement() == temp.GetElement()
                    && GameField.GetGameField()[a, b + 1].GetComponent<Rock>().GetElement() != Debris
                    && temp.GetElement() != Debris
                    && tile.GetElement() != Debris)
                {

                    return true;

                }

                if (a - 1 >= 0)
                {

                    temp = GameField.GetGameField()[a - 1, b + 1].GetComponent<Rock>();
                    if (tile.GetElement() == temp.GetElement()
                        && GameField.GetGameField()[a, b + 1].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {

                        return true;

                    }

                }
                if (b + 2 != _fieldRows)
                {
                    temp = GameField.GetGameField()[a, b + 2].GetComponent<Rock>();
                    if (tile.GetElement() == temp.GetElement()
                        && GameField.GetGameField()[a, b + 1].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {

                        return true;

                    }
                }

            }
        }

            if (b + 1 != _fieldRows)
            {
                tile = GameField.GetGameField()[a, b + 1].GetComponent<Rock>();
                if (tile.GetElement() == _firstRockElement)
                {
                    if (a - 1 >= 0)
                    {
                        Rock temp = GameField.GetGameField()[a - 1, b].GetComponent<Rock>();
                        if (temp.GetElement() == tile.GetElement()
                            && GameField.GetGameField()[a, b + 1].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }
                    }
                    if (a + 1 != 6)
                    {
                        Rock temp = GameField.GetGameField()[a + 1, b].GetComponent<Rock>();
                        if (temp.GetElement() == tile.GetElement()
                            && GameField.GetGameField()[a, b + 1].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }
                    }
                }
            }
            
        
        return false;
    }

    private bool PotentialRowMatches()
    {
        bool potentialMatch = false;

        for (int b = _fieldVisibleRows; b < _fieldRows; b++)
        {

            _firstRockElement = GameField.GetGameField()[0, b].GetComponent<Rock>().GetElement();

            for (int a = 1; a < _fieldColumns; a++)
            {

                if (RowRightCheck(potentialMatch, a, b))
                {
                    return true;
                }
                if (RowLeftCheck(potentialMatch, a - 2, b))
                {
                    return true;
                }
                _firstRockElement = GameField.GetGameField()[a, b].GetComponent<Rock>().GetElement();

            }

        }
        return false;

    }

    private bool RowLeftCheck(bool potentialMatch, int a, int b)
    {
        Rock tile;
        if (a >= 0)
        {
            tile = GameField.GetGameField()[a, b].GetComponent<Rock>();
            if (tile.GetElement() == _firstRockElement
                && !tile.sentToFeedback
                && (tile.GetElement() != Debris
                || _firstRockElement != Debris))
            {
                if (a - 1 >= 0 && b - 1 >= 6)
                {
                    Rock temp = GameField.GetGameField()[a - 1, b - 1].GetComponent<Rock>();

                    if (tile.GetElement() == temp.GetElement()
                        && GameField.GetGameField()[a - 1, b].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {

                        return true;

                    }

                    if (b + 1 != _fieldRows)
                    {

                        temp = GameField.GetGameField()[a - 1, b + 1].GetComponent<Rock>();
                        if (tile.GetElement() == temp.GetElement()
                            && GameField.GetGameField()[a - 1, b].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }

                    }
                    if (a - 2 >= 0)
                    {
                        temp = GameField.GetGameField()[a - 2, b].GetComponent<Rock>();
                        if (tile.GetElement() == temp.GetElement()
                            && GameField.GetGameField()[a - 1, b].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {

                            return true;

                        }

                    }

                }
            }
                if (a - 1 >= 0)
                {
                    tile = GameField.GetGameField()[a - 1, b].GetComponent<Rock>();
                    if (tile.GetElement() == _firstRockElement)
                    {
                        if (b - 1 >= 6)
                        {
                            Rock temp = GameField.GetGameField()[a, b - 1].GetComponent<Rock>();
                            if (temp.GetElement() == tile.GetElement()
                                && GameField.GetGameField()[a - 1, b].GetComponent<Rock>().GetElement() != Debris
                                && temp.GetElement() != Debris
                                && tile.GetElement() != Debris)
                            {
                                return true;

                            }
                        }
                        if (b + 1 != _fieldRows)
                        {
                            Rock temp = GameField.GetGameField()[a, b + 1].GetComponent<Rock>();
                            if (temp.GetElement() == tile.GetElement()
                                && GameField.GetGameField()[a - 1, b].GetComponent<Rock>().GetElement() != Debris
                                && temp.GetElement() != Debris
                                & tile.GetElement() != Debris)
                            {
                                return true;

                            }
                        }
                    }
                }
                

        }
        return false;
    }

    private bool RowRightCheck(bool potentialMatch, int a, int b)
    {
        Rock tile = GameField.GetGameField()[a, b].GetComponent<Rock>();

        if (tile.GetElement() == _firstRockElement
            && !tile.sentToFeedback
            && (tile.GetElement() != Debris
            || _firstRockElement != Debris))
        {
            if (b + 1 != _fieldRows && a + 1 != 6)
            {
                Rock temp = GameField.GetGameField()[a + 1, b + 1].GetComponent<Rock>();

                if (tile.GetElement() == temp.GetElement()
                    && GameField.GetGameField()[a + 1, b ].GetComponent<Rock>().GetElement() != Debris
                    && temp.GetElement() != Debris
                    && tile.GetElement() != Debris)
                {

                    return true;

                }

                if (b - 1 >= 6)
                {

                    temp = GameField.GetGameField()[a + 1, b - 1].GetComponent<Rock>();
                    if (tile.GetElement() == temp.GetElement()
                        && GameField.GetGameField()[a + 1, b].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {

                        return true;

                    }

                }
                if (a + 2 < 6)
                {
                    temp = GameField.GetGameField()[a + 2, b].GetComponent<Rock>();
                    if (tile.GetElement() == temp.GetElement()
                        && GameField.GetGameField()[a + 1, b].GetComponent<Rock>().GetElement() != Debris
                        && temp.GetElement() != Debris
                        && tile.GetElement() != Debris)
                    {

                        return true;

                    }
                }

            }

            if (a + 1 != 6)
            {
                tile = GameField.GetGameField()[a + 1, b].GetComponent<Rock>();
                if (tile.GetElement() == _firstRockElement)
                {
                    if (b - 1 >= 6)
                    {
                        Debug.Log("3 1");
                        Rock temp = GameField.GetGameField()[a, b - 1].GetComponent<Rock>();
                        if (temp.GetElement() == tile.GetElement()
                            && GameField.GetGameField()[a + 1, b].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {
                            Debug.Log("3");
                            return true;

                        }
                    }
                    if (b + 1 != _fieldRows)
                    {
                        Rock temp = GameField.GetGameField()[a, b + 1].GetComponent<Rock>();
                        if (temp.GetElement() == tile.GetElement()
                            && GameField.GetGameField()[a + 1, b].GetComponent<Rock>().GetElement() != Debris
                            && temp.GetElement() != Debris
                            && tile.GetElement() != Debris)
                        {
                            Debug.Log("4 1");
                            Debug.Log("4");
                            return true;

                        }
                    }
                }
            }

        }

        return false;
    }
    private void ResetTimer()
    {
        _timer.Stop();
        _timer.SetTime(0.6f);
        _timer.StartTimer();
    }
}