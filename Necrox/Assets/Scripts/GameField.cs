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
	private Randomizer rand = new Randomizer();
	private bool startGame = true;
	private static int rows;
	private float timer = 0.55f;
	private static int column;
	private float count;
	private int rowsDone;
	private string element;
	private List<GameObject> Children = new List<GameObject>();


	void Start () {
		rows = arrayRows;
		column = arrayColumns;
		count = timer;
		rowsDone = 0;
		
        foreach (Transform child in transform) {

			Children.Add(child.gameObject);
    	}

		gameField = new GameObject[arrayColumns, arrayRows];
		Time.timeScale = 1;
	}
	
	void Update () {
		if (startGame) {
			if (timer <= count) {
				int a = 0;
				foreach (GameObject child in Children) {
					child.GetComponent<ColumnBehaviour>().CreateRock(a,(arrayRows-1) - rowsDone,element);
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
			for (int a = 0; a < gameField.GetLength(0);a++) {
				Debug.Log("Column " + a);
				for (int b = 0; b < gameField.GetLength(1);b++) {
					Debug.Log(gameField[a,b]);
				}
				Debug.Log("");
			}
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
		switch(element.ToLower()) {
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
