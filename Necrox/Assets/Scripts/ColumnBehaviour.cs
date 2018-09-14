using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBehaviour : MonoBehaviour {

	public int rowNumber;

	ColumnBehaviour() {
	}

	void OnDrawGizmosSelected() {
			Gizmos.color = new Color(1,0,0,0.5f);
			Gizmos.DrawCube(transform.parent.position + transform.localPosition + new Vector3(0,0.5f,0)
							,new Vector3(1.25f,9f,0));
		}

	public void CreateRock() {
		var newRock = Instantiate(GameField.GetRockPrefab(),
								  new Vector3(transform.position.x,1,-1),
								  Quaternion.identity);
		
		newRock.transform.parent = gameObject.transform;
		newRock.Init((int) transform.localPosition.x, GameField.GetArrayRows());
		
	}

	public void FillColumn() {
		for (int a = 0 ; GameField.GetArrayRows() > a ; a++) {
			var newRock = Instantiate(GameField.GetRockPrefab(),
									  new Vector3(transform.position.x,1,-1),
									  Quaternion.identity);
									  
			newRock.transform.parent = gameObject.transform;
			newRock.Init((int) transform.localPosition.x - a, GameField.GetArrayRows());
		}
	}
}
