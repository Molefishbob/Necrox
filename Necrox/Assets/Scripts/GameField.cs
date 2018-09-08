using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

	public Vector2 center;
	public Vector2 size;
	public Rock rockPrefab;
	private char[,] gameField;
	private Randomizer rand = new Randomizer();
	private Vector2 topLeft;


	void Start () {
		gameField = new char[(int) size.x, (int) size.y];
		topLeft = new Vector2(0,-size.y/2);
		FillArea();
		Time.timeScale = 1;
	}
	
	void Update () {
		Debug.Log("topleft:" + topLeft);
	}

		public void SpawnRocks(int a, int b) {
			Vector2 pos = topLeft;

			Instantiate(rockPrefab,pos,Quaternion.identity).Initialize(a, b);
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = new Color(1,0,0,0.5f);
			Gizmos.DrawCube(center,size);
		}

		private void FillArea() {
			/*
			for (int a = 0; a < gameField.length;a++) {
				for (int b =0; b < gameField.length; b++) {
					if (gameField[a,b] == 0) {
						FillSpot(a, b);
					}
				}
			}
			*/
		}
		private void FillSpot(int a , int b) {
			gameField[a,b] = rand.RandomRock();
			SpawnRocks(a, b);
		}
}
