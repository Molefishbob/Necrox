using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBehaviour : MonoBehaviour {

	public int rowNumber;
	public GameField gameField;
	ColumnBehaviour() {
	}

	void OnDrawGizmosSelected() {

			Gizmos.color = new Color(1,0,0,0.5f);
			Gizmos.DrawCube(transform.parent.position + transform.localPosition + new Vector3(0,0.5f,0)
							,new Vector3(1f,6f,0));

		}

	public void CreateRock(int a, int b, string element) {



		var newRock = Instantiate(gameField.GetRockPrefab(element),
								  new Vector3(transform.position.x,0.85f,-1),
								  Quaternion.identity);
		
		newRock.transform.parent = gameObject.transform;
		GameField.setObject(a,b,newRock.gameObject);
		newRock.Init( transform.position.x, b, element);
		
	}
}
