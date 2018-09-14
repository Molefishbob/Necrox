using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

	public Vector2 center;
	public Vector2 size;
	public Rock rockPrefab;
	public int arrayRows;
	public int arrayColumns;
	public static GameObject[,] gameField;

	private Randomizer rand = new Randomizer();
	private bool startGame = true;
	private static int rows;
	private float timer = 0.2f;
	private static int column;
	private static Rock rock;
	private float count;
	private int rowsDone;
	private List<GameObject> Children = new List<GameObject>();


	void Start () {
		rows = arrayRows;
		rock = rockPrefab;
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
			if (timer + Random.Range(0.2f,0.5f) <= count) {
				foreach (GameObject child in Children) {
						child.GetComponent<ColumnBehaviour>().CreateRock();
				}
				rowsDone++;
				if (rowsDone == arrayRows) {
					startGame = false;
				}
				count = 0;
			}
			count = count + Time.deltaTime;
		}
	}

	public static Rock GetRockPrefab() {
		return rock;
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
	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1,0,0,0.5f);
		Gizmos.DrawCube(center,size);
	}
}
