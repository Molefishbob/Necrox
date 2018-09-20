using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

	public Vector2 center;
	public Vector2 size;
	public int arrayRows;
	public int arrayColumns;
	public static GameObject[,] gameField;
	public Rock waterRock;
	public Rock fireRock;
	public Rock earthRock;
	public Rock chaosRock;
	public Rock corruptionRock;
	public Templates template;
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
