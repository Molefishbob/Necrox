using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBehaviour : MonoBehaviour {

	public int rowNumber;
	public GameField gameField;
	public bool tileSwitchDisabled {
		get;
		set;
	}

	void Update() {
		
		if(tileSwitchDisabled) {

			Transform[] allChildren = GetComponentsInChildren<Transform>();
			List<Rock> childObjects = new List<Rock>();

			foreach (Transform child in allChildren) { 

				childObjects.Add(child.GetComponent<Rock>());

			}

			bool switchNotAllowed = false;

			foreach (Rock tile in childObjects)
			{
			
				if (tile != null) {

					bool moving = tile.GetMoved();
					bool beingDestroyed = tile.sentToFeedback;

					if (moving || beingDestroyed) {
						switchNotAllowed = true;
					}

				}

			}
			
			if (!switchNotAllowed) {

				tileSwitchDisabled = false;

			}

		}

	}

	void OnDrawGizmosSelected() {

			Gizmos.color = new Color(1,0,0,0.5f);
			Gizmos.DrawCube(transform.parent.position + transform.localPosition + new Vector3(0,0.5f,0)
							,new Vector3(1f,6f,0));

	}

    public void CreateExtraRock(int a, int b, string element) {

		var newRock = Instantiate(gameField.GetRockPrefab(element),
								  new Vector3(transform.position.x,6.85f - b,-1),
								  Quaternion.identity);
		
		newRock.transform.parent = gameObject.transform;
		GameField.setObject(a,b,newRock.gameObject);
		newRock.Init( transform.position.x, b, element);

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
