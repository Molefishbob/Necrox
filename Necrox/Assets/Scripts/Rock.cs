using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public int[] pos = new int[2];
    private bool _fallingToPlace;
    [SerializeField]
    private bool _moved;
    public string _element;
    private int _yPosition;
    private int _xPosition = 0;
    private bool _xValueChanged;
    private bool _yValueChanged;
    private bool _toBeDestroyed;
    private GameField _gameField;
    private float speed = 0.25f;
    public GameObject feedback;
    public bool Paused {
        get;
        set;
    }
    public bool sentToFeedback {
        get; 
        private set;
    }


    // Use this for initialization
    void Start()
    {
        _gameField = transform.parent.parent.GetComponent<GameField>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!Paused) {
            if (_fallingToPlace)
            {
                transform.localPosition = new Vector3(0, transform.localPosition.y - speed, 0);
                if (transform.localPosition.y <= _yPosition)
                {
                    _fallingToPlace = false;
                    //Debug.Log(pos[0] + " " + pos[1]);
                }
            }
            if (_moved && !_fallingToPlace)
            {
                TileMovement();
            }
            if (pos[1] < 11 && !_fallingToPlace) {
                if (GameField.GetGameField()[pos[0],pos[1] + 1] == null && !_gameField.GetFirstTable()) {
                    _gameField.ClearTileFromField(pos[0],pos[1]);
                    pos[1] = pos[1]+ 1;
                    GameField.setObject(pos[0],pos[1],gameObject);
                    _yPosition = ToPosValues(pos[1]);
                    //Debug.Log("I'm moving to: X:" + pos[0] + " Y:" + pos[1] + " Element:" + _element);
                    _yValueChanged = true;
                    _moved = true;
                }
            }
            IsDestroyable();
        }
    }

    private void IsDestroyable() {
        if (pos[1] < GameField.GetArrayRows()) {
            gameObject.GetComponent<Collider2D>().enabled = false;
        } else {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void TileMovement()
    {
        if (_xValueChanged)
        {
            XMovement();
        }

        if (_yValueChanged)
        {
            YMovement();
        }

        if (!_xValueChanged && !_yValueChanged)
        {
            _moved = false;
        }
    }

    public void Init(float x, int y, string element)
    {
        int intX = Mathf.CeilToInt(x);

        intX = ToArrayValues(intX);

        pos = new int[] { intX, y };

        //Debug.Log(string.Format("X: {0} Y: {1}",pos[0],pos[1]));

        y = ToPosValues(y);

        _yPosition = y;

        this._element = element;
        _fallingToPlace = true;
    }

    public void ChangeParent(GameObject column)
    {

        //Debug.Log("Old Parent: " + transform.parent.GetComponent<ColumnBehaviour>().rowNumber +
        //" New Parent: " + column.GetComponent<ColumnBehaviour>().rowNumber);
        //Debug.Log("X:"+ pos[0] + " Y:" + pos[1]);

        transform.parent = column.transform;
    }

    private int ToPosValues(int value)
    {
        switch (value)
        {
            case 11:
                return -2;
            case 10:
                return -1;
            case 9:
                return 0;
            case 8:
                return 1;
            case 7:
                return 2;
            case 6:
                return 3;
            case 5:
                return 4;
            case 4:
                return 5;
            case 3:
                return 6;
            case 2:
                return 7;
            case 1:
                return 8;
            case 0:
                return 9;
            default:
                Debug.LogError("Mistakes: Y-pos of tile");
                return 3;
        }
    }
    private int ToArrayValues(int value)
    {
        switch (value)
        {
            case 3:
                return 5;
            case 2:
                return 4;
            case 1:
                return 3;
            case 0:
                return 2;
            case -1:
                return 1;
            case -2:
                return 0;
            default:
                Debug.LogError("Mistakes: X-pos of tile");
                return 5;

        }
    }

    private void YMovement()
    {
        if (_yPosition < transform.localPosition.y)
        {

            transform.localPosition = new Vector3(0,
                                                  transform.localPosition.y - speed,
                                                  0);

            if (_yPosition >= transform.localPosition.y)
            {
                _yValueChanged = false;
                //Debug.Log("YValue correct");
            }
        }
        if (_yPosition > transform.localPosition.y)
        {

            transform.localPosition = new Vector3(0,
                                                  transform.localPosition.y + speed,
                                                  0);

            if (_yPosition <= transform.localPosition.y)
            {
                _yValueChanged = false;
                //Debug.Log("YValue correct");
            }
        }
    }

    private void XMovement()
    {
        // Debug.Log(string.Format("DX:{0} DY:{1} RealX:{2} RealY:{3}",
        //                                     _xPosition,
        //                                     _yPosition,
        //                                     transform.localPosition.x,
        //                                     transform.localPosition.y));

        if (_xPosition < transform.localPosition.x)
        {

            transform.localPosition = new Vector3(transform.localPosition.x - 0.125f,
                                                  transform.localPosition.y,
                                                  0);

            if (_xPosition >= transform.localPosition.x)
            {
                _xValueChanged = false;
                //Debug.Log("XValue correct");
            }
        }

        if (_xPosition > transform.localPosition.x)
        {

            transform.localPosition = new Vector3(transform.localPosition.x + 0.125f,
                                                  transform.localPosition.y,
                                                  0);

            if (_xPosition <= transform.localPosition.x)
            {
                _xValueChanged = false;
                //Debug.Log("XValue correct");
            }
        }
    }
    public void DestroyTile() {
        if (_toBeDestroyed) {
            
            if (pos[1] >= 6) {
                // GameObject.Find("GameLogic").GetComponent<GameLogic>().TileWasDestroyed();
            }
            if (!sentToFeedback) {
                Vector3 tilePos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                GameObject.Find("Feedback").GetComponent<Feedback>().TileFeedback(tilePos, gameObject);
                sentToFeedback = true;
            }
            Debug.Log(pos[0] + " " + pos[1]);
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject,0.75f);
        }
    }
    public void DestroyExtraTile() {
        if (_toBeDestroyed) {

            if (pos[1] >= 6) {
                // GameObject.Find("GameLogic").GetComponent<GameLogic>().TileWasDestroyed();
            }
            Destroy(gameObject, 0.75f);
        }
    }

    public void SetPos(int[] pos)
    {
        if (this.pos[0] != pos[0])
        {
            _xValueChanged = true;
            _moved = true;
        }
        if (this.pos[1] != pos[1])
        {
            _yValueChanged = true;
            _moved = true;
        }
        this.pos = pos;
        _yPosition = ToPosValues(pos[1]);
    }
    public void SetToBeDestroyed(bool destroy) {
        _toBeDestroyed = destroy;
    }
    public bool GetToBeDestroyed() {
        return _toBeDestroyed;
    }
    public bool GetMoved() {
        return _moved;
    }
    public bool GetFalling() {
        return _fallingToPlace;
    }

    public int[] GetPos()
    {
        return pos;
    }
    public string GetElement()
    {
        return _element;
    }
}