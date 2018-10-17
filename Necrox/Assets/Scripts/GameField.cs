using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

	public Vector2 center;
	public Vector2 size;
	public int arrayRows;
	public int arrayColumns;
	public static GameObject[,] gameField;
	public FieldReader _fieldReader;
	public Rock waterRock;
	public Rock fireRock;
	public Rock earthRock;
	public Rock chaosRock;
	public Rock corruptionRock;
	public Templates template;
	public GameObject column0;
	public GameObject column1;
	public GameObject column2;
	public GameObject column3;
	public GameObject column4;
	public GameObject column5;
	private Randomizer rand = new Randomizer();
	private bool startGame = true;
	private static int rows;
	private float timer = 0.25f;
	private static int column;
	private float count;
	private int rowsDone;
	private string[,] row;
	private List<GameObject> Children = new List<GameObject>();
	


	void Start () {
		row = new string[6,6];
		rows = arrayRows;
		column = arrayColumns;
		count = timer;
		rowsDone = 0;
		_fieldReader = GetComponent<FieldReader>();
		
        foreach (Transform child in transform) {

			Children.Add(child.gameObject);
    	}
		CreateRandomRow(template.GetRandomRowTemplate());

		gameField = new GameObject[arrayColumns, arrayRows];
		Time.timeScale = 1;
	}
	
	void Update () {
		if (startGame) {
			if (timer <= count) {
				int a = 0;
				foreach (GameObject child in Children) {
					child.GetComponent<ColumnBehaviour>().CreateRock(a,(arrayRows-1) - rowsDone,row[rowsDone,a]);
					a++;
				}
				rowsDone++;
				if (rowsDone == arrayRows) {
					startGame = false;
				}
				count = 0;
			}
			count = count + Time.deltaTime;
		}
		if (Input.GetKeyDown("space")) {
			for (int a = 0; a < row.GetLength(0);a++) {
				Debug.Log("Column " + a);
				for (int b = 0; b < row.GetLength(1);b++) {
					Debug.Log(row[a,b]);
				}
				Debug.Log("");
			}
		}
	}
	
	public void CreateRandomRow(string str) {
		int count = 0;
		for (int a = 0; count < arrayColumns; a = a+6) {
			for (int b = 0; b < arrayRows;b++) {

				switch (str[a+b]) {

					case 'f':
						row[count,b] = "fire";
						break;
					case 'w':
						row[count,b] = "water";
						break;
					case 'e':
						row[count,b] = "earth";
						break;
					case 'c':
						row[count,b] = "chaos";
						break;
					default:
						row[count,b] = "fire";
						break;

				}
			}
			count++;
		}
	}

	public void MoveTiles(GameObject rock1, GameObject rock2) {

		Rock rocker1 = rock1.GetComponent<Rock>();
		Rock rocker2 = rock2.GetComponent<Rock>();
		int[] posRock1 = rocker1.GetPos();

		rocker1.SetPos(rocker2.GetPos());
		rocker2.SetPos(posRock1);

		gameField[rocker1.pos[0],rocker1.pos[1]] = rock1;
		gameField[rocker2.pos[0],rocker2.pos[1]] = rock2;
		
		// for (int a = 0; a < row.GetLength(0);a++) {
		// 		Debug.Log("Column " + a);
		// 		for (int b = 0; b < row.GetLength(1);b++) {
		// 			Debug.Log(row[a,b]);
		// 		}
		// 		Debug.Log("");
		// 	}

		rocker1.ChangeParent(FindParent(rocker1.GetPos()[0]));
		rocker2.ChangeParent(FindParent(rocker2.GetPos()[0]));
	}
	
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

	public void ClearTileFromField(int a, int b) {
		gameField[a,b] = null;
	}

	public static GameObject[,] GetGameField() {
		return gameField;
	}
	
	public static int GetArrayColumns() {
		return column;
	}
	public static int GetArrayRows() {
		return rows;
	}
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
			case "corruption":
				return corruptionRock;
			default:
				Debug.LogError("Element (string) is not valid in tile");
				return fireRock;
		}
	}
	public static void setObject(int a, int b, GameObject rock) {
		gameField[a,b] = rock;
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1,0,0,0.5f);
		Gizmos.DrawCube(center,size);
	}
}
