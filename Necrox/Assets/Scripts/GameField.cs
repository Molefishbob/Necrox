using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

	public Vector2 center;
	public Vector2 size;
	public Rock rockPrefab;
	public int arrayRows;
	public int arrayColumns;
	public static char[,] gameField;
	private Randomizer rand = new Randomizer();
	private static int rows;
	private static int column;
	private static Rock rock;
	private List<GameObject> Children = new List<GameObject>();


	void Start () {
		rows = arrayRows;
		rock = rockPrefab;
		column = arrayColumns;
		
        foreach (Transform child in transform) {
			Children.Add(child.gameObject);
			child.GetComponent<ColumnBehaviour>().FillColumn();
         }

		gameField = new char[arrayColumns, arrayRows];
		Time.timeScale = 1;
	}
	
	void Update () {

	}

	public static Rock GetRockPrefab() {
		return rock;
	}
	public static char[,] GetGameField() {
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
/*
		private void FillArea() {
			for (int a = 0; a < gameField.Length;a++) {
				for (int b =0; b < gameField.Length; b++) {
					if (gameField[a,b] == 0) {
						FillSpot(a, b);
					}
				}
			}
		}
		private void FillSpot(int a , int b) {
			gameField[a,b] = rand.RandomRock();
			SpawnRocks(a, b);
		}
		*/
}
