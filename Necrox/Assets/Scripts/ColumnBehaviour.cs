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
							,new Vector3(6,0.7f,0));
		}

	public void FillColumn() {
		for (int a = 0 ; GameField.GetArrayRows() > a ; a++) {
			var newRock = Instantiate(GameField.GetRockPrefab(),
									  new Vector3(a,0,0),
									  Quaternion.identity);
									  
			newRock.transform.parent = gameObject.transform;
			newRock.Init(GameField.GetArrayColumns(), (int) transform.position.y -a);
		}
	}
}
