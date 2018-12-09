using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main class for handling everything the ingame tiles do
/// and how they act in different situtations
/// <summary>
public class Rock : MonoBehaviour
{
    private const string Debris = "debris";
    public int[] pos = new int[2];
    private bool _fallingToPlace;
    [SerializeField]
    private bool _moved;
    public string _element;
    private int _yPosition;
    private int _xPosition = 0;
    private bool _xValueChanged;
    private bool _yValueChanged;
    public bool _toBeDestroyed;
    private GameField _gameField;
    private float speed = 0.25f;
    public GameObject feedback;
    public bool sentToFeedback
    {
        get;
        private set;
    }
    public bool isImmovable
    {
        get;
        set;
    }


    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        _gameField = transform.parent.parent.GetComponent<GameField>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void FixedUpdate()
    {
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
        if (pos[1] < 11 && !_fallingToPlace)
        {

            if (GameField.GetGameField()[pos[0], pos[1] + 1] == null && !_gameField.GetFirstTable())
            {

                _gameField.ClearTileFromField(pos[0], pos[1]);
                pos[1] = pos[1] + 1;
                GameField.setObject(pos[0], pos[1], gameObject);
                _yPosition = ToPosValues(pos[1]);
                //Debug.Log("I'm moving to: X:" + pos[0] + " Y:" + pos[1] + " Element:" + _element);
                _yValueChanged = true;
                _moved = true;

            }

        }
        IsDestroyable();
    }

    /// <summary>
    /// Checks if the tile is in the destroyable part of the gamefield.
    /// </summary>
    private void IsDestroyable()
    {
        if (pos[1] < GameField.GetArrayRows())
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    /// <summary>
    /// Checks if the tile has been told to move 
    /// on the X or/and the Y - axis.
    /// </summary>
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

    /// <summary>
    /// The initial setup for the tile
    /// </summary>
    /// <param name="x">The X-axis position</param>
    /// <param name="y">The Y-axis position</param>
    /// <param name="element">The tiles element</param>
    public void Init(float x, int y, string element)
    {
        int intX = Mathf.CeilToInt(x);

        intX = ToArrayValues(intX);

        if (element == Debris)
        {
            isImmovable = true;
        }

        pos = new int[] { intX, y };

        y = ToPosValues(y);

        _yPosition = y;

        this._element = element;
        _fallingToPlace = true;
    }

    /// <summary>
    /// A method for changing the parent column for the tile.
    /// Used when the tile moves on the X-axis.
    /// </summary>
    /// <param name="column">The columns gameobject reference</param>
    public void ChangeParent(GameObject column)
    {

        //Debug.Log("Old Parent: " + transform.parent.GetComponent<ColumnBehaviour>().rowNumber +
        //" New Parent: " + column.GetComponent<ColumnBehaviour>().rowNumber);
        //Debug.Log("X:"+ pos[0] + " Y:" + pos[1]);

        transform.parent = column.transform;

    }

    /// <summary>
    /// Changes an array value to a position value inside the game.
    /// </summary>
    /// <param name="value">The arrays value</param>
    /// <returns>Returns the corresponding value</returns>
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

    /// <summary>
    /// Changes the given value to corresponding values
    /// for the array
    /// </summary>
    /// <param name="value">The given axis value</param>
    /// <returns>Returns the corresponding value</returns>
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

    /// <summary>
    /// The logic for movement on the Y -axis
    /// </summary>
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

    /// <summary>
    /// The logic for the movement on the X-axis.
    /// </summary>
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

    /// <summary>
    /// The method for destroying the tile
    /// Also prepares the instantiation of the feedback
    /// for the tile being destroyed.
    /// </summary>
    public void DestroyTile()
    {
        if (_toBeDestroyed && !isImmovable)
        {

            if (pos[1] >= 6)
            {
                // GameObject.Find("GameLogic").GetComponent<GameLogic>().TileWasDestroyed();
            }
            if (!sentToFeedback)
            {
                Vector3 tilePos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                GameObject.Find("Feedback").GetComponent<Feedback>().TileFeedback(tilePos, gameObject);
                if (!this.GetComponentInParent<ColumnBehaviour>().tileSwitchDisabled)
                {
                    this.GetComponentInParent<ColumnBehaviour>().tileSwitchDisabled = true;
                }
                sentToFeedback = true;
            }
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 0.75f);
        }
    }

    /// <summary>
    /// Destroys the tile if it is set to be destroyed
    /// and above the play area
    /// </summary>
    public void DestroyExtraTile()
    {
        if (_toBeDestroyed)
        {

            if (pos[1] >= 6)
            {
                // GameObject.Find("GameLogic").GetComponent<GameLogic>().TileWasDestroyed();
            }
            Destroy(gameObject, 0.75f);
        }
    }

    /// <summary>
    /// Method for setting a new position value for the tile
    /// </summary>
    /// <param name="pos">The new position in X and Y coordinates</param>
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

    /// <summary>
    /// Method to set the tile to be destroyed.
    /// </summary>
    /// <param name="destroy">The value the boolean will be set to</param>
    public void SetToBeDestroyed(bool destroy)
    {
        _toBeDestroyed = destroy;
    }

    /// <summary>
    /// Method to get if the tile is moving inside the playarea.
    /// </summary>
    /// <returns>The value of the bool</returns>
    public bool GetMoved()
    {
        return _moved;
    }

    /// <summary>
    /// Is the tile moving outside of the playarea.
    /// </summary>
    /// <returns>Returns the value of the bool</returns>
    public bool GetFalling()
    {
        return _fallingToPlace;
    }

    /// <summary>
    /// Method to get the position of the tile
    /// </summary>
    /// <returns>Returns the int array</returns>
    public int[] GetPos()
    {
        return pos;
    }

    /// <summary>
    /// Method to get the tiles element.
    /// </summary>
    /// <returns>Returns the element in string</returns>
    public string GetElement()
    {
        return _element;
    }
}