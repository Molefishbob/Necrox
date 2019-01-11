using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class creates the tiles in to the gamefield and holds all related information to these tiles.
/// </summary>
public class GameField : MonoBehaviour {

	public Camera _camera;

	public int arrayRows;
	public int arrayColumns;
	public static GameObject[,] gameField;
	public Rock waterRock;
	public Rock fireRock;
	public Rock earthRock;
	public Rock chaosRock;
	public Rock debrisRock;
	public Templates template;
	public GameObject column0;
	public GameObject column1;
	public GameObject column2;
	public GameObject column3;
	public GameObject column4;
	public GameObject column5;
    public AudioClip _movementAudio;
    public GameLogic _gameLogic;
	
    public bool _useStandardTemplates;

	private bool startGame = true;
	private static int rows;
	private float timer = 0.1f;
	private float count;
	private int rowsDone;
	private string[,] _firstField;
	private string[,] _extraTiles;
	private bool newExtraTable;

	private List<GameObject> Children = new List<GameObject>();
    private bool emptyAreas;
    private bool firstTable = true;

    public bool _creatingNewTiles { get; internal set; }

    void Start () {
		GameManager.defaultMultiplier = 100;
		_firstField = new string[6,6];
		_extraTiles = new string[6,6];
		rows = arrayRows;
		count = timer;
		rowsDone = 0;
		
        foreach (Transform child in transform) {

			Children.Add(child.gameObject);
    	}
		if (_useStandardTemplates) {
			CreateRandomRow(template.GetRandomRowTemplate(),1);
		} else {
			CreateRandomRow(template.getRandomRowDebrisTemplate(),1);
		}
		CreateRandomRow(template.GetRandomRowTemplate(),2);

		gameField = new GameObject[arrayColumns, arrayRows*2];
		Time.timeScale = 1;
	}
	
	void FixedUpdate () {
		if (startGame)
        {
            ConfigureGame();
        }

		for(int a = 0; a < arrayColumns && !newExtraTable && !startGame;a++) {
			int count = 0;
			for(int b = 0; b < arrayRows; b++) {
				if (gameField[a,b] == null) {
					count++;
				}
				if (count >= arrayRows) {
					for(int c = 0; c < arrayColumns;c++) {
						for(int d = 0; d < arrayRows; d++) {
							if (gameField[c,d] != null) {
								gameField[c,d].GetComponent<Rock>().SetToBeDestroyed(true);
								gameField[c,d].GetComponent<Rock>().DestroyExtraTile();
								//gameField[c,d] = null;
							}
						}
					}
					CreateRandomRow(template.GetRandomRowTemplate(),2);
					startGame = true;
					newExtraTable = true;
				}
			}
		}


        if (Input.GetKeyDown("space")) {
			//System.Random random = new System.Random();
			//int a = random.Next(0,6);
			//int b = random.Next(6,12);
			//if(gameField[a,b] != null) {
			//	Destroy(gameField[a,b]);
			//}

			for (int a = 0; a < gameField.GetLength(0);a++) {
				Debug.Log("Column:" + a);
				for (int b = 0; b < gameField.GetLength(1);b++) {
					Debug.Log(gameField[a,b].GetComponent<Rock>().GetElement());
				}
			}
		}
	}

	/// <summary>
	/// Configures the game at the start of the game.
	/// Spawns the tiles for every field in the 2D array.
	/// </summary>
    private void ConfigureGame()
    {
        if (firstTable)
        {
            if (timer <= count)
            {
                int a = 0;
                foreach (GameObject child in Children)
                {
                    child.GetComponent<ColumnBehaviour>().CreateRock(
                                                                    a, (arrayRows + 5) - rowsDone,
                                                                    _firstField[rowsDone, a]);
                    a++;
                }
                rowsDone++;
                if (rowsDone == arrayRows)
                {
                    firstTable = false;
                    rowsDone = 0;
                }
                count = 0;
            }
        }
        count = count + Time.deltaTime;
        if (!firstTable)
        {
            if (timer <= count)
            {
                int a = 0;
                foreach (GameObject child in Children)
                {
                    child.GetComponent<ColumnBehaviour>().CreateExtraRock(
                                                                    a, (arrayRows - 1) - rowsDone,
                                                                    _extraTiles[rowsDone, a]);
                    a++;
                }
                rowsDone++;
                if (rowsDone == arrayRows)
                {
                    startGame = false;
					newExtraTable = false;
                    _gameLogic._paused = false;
                    _creatingNewTiles = false;
					rowsDone = 0;
                }
                count = 0;
            }
            count = count + Time.deltaTime;
        }
    }

	/// <summary>
	/// Decodes a string to create a row of tiles.
	///	The number determines the array for the tile.
	/// </summary>
	/// <param name="str">The row in a string</param>
	/// <param name="number">Which array it belongs to</param>
    public void CreateRandomRow(string str,int number) {
		int count = 0;
		// It is disgusting, I know.
		for (int a = 0; count < arrayColumns; a = a+6) {
			for (int b = 0; b < arrayRows;b++) {

				switch (str[a+b]) {

					case 'f':
						if (number == 1) {
							_firstField[count,b] = "fire";
						}else if (number == 2) {
							_extraTiles[count,b] = "fire";
						}
						break;
					case 'w':
						if (number == 1) {
							_firstField[count,b] = "water";
						}else if (number == 2) {
							_extraTiles[count,b] = "water";
						}
						break;
					case 'e':
						if (number == 1) {
							_firstField[count,b] = "earth";
						}else if (number == 2) {
							_extraTiles[count,b] = "earth";
						}
						break;
					case 'c':
						if (number == 1) {
							_firstField[count,b] = "chaos";
						}else if (number == 2) {
							_extraTiles[count,b] = "chaos";
						}
						break;
					case 'd':
						if (number == 1) {
							_firstField[count,b] = "debris";
						}else if (number == 2) {
							_extraTiles[count,b] = "debris";
						}
						break;
					default:
						if (number == 1) {
							_firstField[count,b] = "fire";
						} else if (number == 2) {
							_extraTiles[count,b] = "fire";					}
						break;

				}
			}
			count++;
		}
	}

	/// <summary>
	/// The method to tell the tiles to switch places when swiped.
	/// </summary>
	/// <param name="rock1">The first rock</param>
	/// <param name="rock2">The second rock</param>
	/// <param name="newMove">If the tiles are not reverting back to place</param>
	public void MoveTiles(GameObject rock1, GameObject rock2, bool newMove) {

		Rock rocker1 = rock1.GetComponent<Rock>();
		Rock rocker2 = rock2.GetComponent<Rock>();
		//Debug.Log(posRock1[0] + " " + posRock1[1]);

		int[] posRock1 = rocker1.GetPos();
		rocker1.SetPos(rocker2.GetPos());
		rocker2.SetPos(posRock1);
		
		
		rocker1.ChangeParent(FindParent(rocker1.GetPos()[0]));
		rocker2.ChangeParent(FindParent(rocker2.GetPos()[0]));

		gameField[rocker1.pos[0],rocker1.pos[1]] = rock1;
		gameField[rocker2.pos[0],rocker2.pos[1]] = rock2;

		if(newMove) {
		    //Debug.Log("Calling to check matches");
			_camera.GetComponent<CameraManager>()
				   	.PlaySound(_movementAudio,GameManager._soundVolume,usePitchVariance: true);
         	gameObject.GetComponent<MatchChecker>().MatchCheck(rock1,rock2);
		 }


    }
	
	/// <summary>
	/// Finds the parent of a tile.
	/// Used to get the reference for a specific column.
	/// </summary>
	/// <param name="y">The y value from the tiles array position</param>
	/// <returns>Returns the reference for the column</returns>
	public GameObject FindParent(int y) {
		switch(y) {
			case 0:
				return column0;
			case 1:
				return column1;
			case 2:
				return column2;
			case 3:
				return column3;
			case 4:
				return column4;
			case 5:
				return column5;
			default:
				Debug.LogError("GameField/FindParent: Mistake in Y-value");
				return column0;
		}
	}

	/// <summary>
	/// Checks if any of the visible tiles are moving.
	/// </summary>
	/// <returns>Returns true if they are moving</returns>
	public bool AreVisibleTilesMoving() {
        
		for (int a = 0; a < gameField.GetLength(0);a++) {
			for ( int b = 6; b < gameField.GetLength(1);b++) {
                if (gameField[a, b] != null) {

                    bool moving = gameField[a, b].GetComponent<Rock>().GetMoved();
					bool falling = gameField[a, b].GetComponent<Rock>().GetFalling();
                    if (moving || falling) {
                        return true;
                    }
                }
			}
		}
		return false;
	}

	/// <summary>
	/// Removes a tile from the gamefield.
	/// </summary>
	/// <param name="a">The x position in array</param>
	/// <param name="b">The y position in array</param>
	public void ClearTileFromField(int a, int b) {
		gameField[a,b] = null;
	}

	/// <summary>
	/// Gives a reference of the 2D array with all the tiles gameobjects.
	/// </summary>
	/// <returns>a 2D array reference</returns>
	public static GameObject[,] GetGameField() {
		return gameField;
	}

	/// <summary>
	/// Returns the amount of rows in the gamefield.
	/// </summary>
	/// <returns>The amount of rows</returns>
	public static int GetArrayRows() {
		return rows;
	}

	/// <summary>
	/// Used to register if any sets of tiles have already been created.
	/// This goes to false after the game has been configured.
	/// </summary>
	/// <returns>If there have already been values in the array</returns>
	public bool GetFirstTable() {
		return firstTable;
	}

	/// <summary>
	/// Used to find the correct prefab for the element in question.
	/// </summary>
	/// <param name="element">The element of the tile</param>
	/// <returns>Reference to the prefab</returns>
	public Rock GetRockPrefab(string element) {
		switch(element) {
			case "water":
				return waterRock;
			case "fire":
				return fireRock;
			case "earth":
				return earthRock;
			case "chaos":
				return chaosRock;
			case "debris":
				return debrisRock;
			default:
				Debug.LogError("Element (string) is not valid in tile");
				return fireRock;
		}
	}
	
	/// <summary>
	/// Sets the given object to the gamefield array into the desired spot.
	/// </summary>
	/// <param name="a">The x position</param>
	/// <param name="b">The y position</param>
	/// <param name="rock">The gameobject of the tile</param>
	public static void setObject(int a, int b, GameObject rock) {
		gameField[a,b] = rock;
		// if (rock.GetComponent<Rock>().GetElement().Length > 1) {
		// 	Debug.Log("X:" + a + " Y:" + b + " Element:" + rock.GetComponent<Rock>().GetElement());
		// }
	}

    internal void CreateNewArrays(bool destroyOldOnes)
    {
        if (destroyOldOnes)
        {
            for (int a = 0; a < gameField.GetLength(0); a++)
            {
                for (int b = 0; b < gameField.GetLength(1); b++)
                {
                    Destroy(gameField[a, b]);
                }
            }
        }
        if (_useStandardTemplates)
        {
            CreateRandomRow(template.GetRandomRowTemplate(), 1);
        }
        else
        {
            CreateRandomRow(template.getRandomRowDebrisTemplate(), 1);
        }
        CreateRandomRow(template.GetRandomRowTemplate(), 2);

        startGame = true;
        firstTable = true;
        _creatingNewTiles = true;
        FindObjectOfType<GameLogic>()._paused = false;
        FindObjectOfType<CombatUI>()._paused = false;
    }
}
