using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldReader : MonoBehaviour {

	// private GameField gameField;
    // private GameObject currentHorTile;
    // private GameObject currentVerTile;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
		//gameField = GetComponent<GameField>();
	}

	/*
	public bool ReadField () {
		
		int countHorizontal = 0;
		int countVertical = 0;
		GameObject[,] suicidalMan =  GameField.GetGameField();

		// no idea how to call/get 2darray from gamefield "GameObject[,]" properly

		for (int i = 0; i < suicidalMan.GetLength(0); i++) {
			for (int j = 0; j < suicidalMan.GetLength(0); j++) {
				string currentElementH = suicidalMan[0,0].GetComponent<Rock>().GetElement(); // What is in the row [0,0], char,string,object? What i'm going to compare?
				string currentElementV = currentElementH;

				if (j + 1 < suicidalMan.GetLength(0)) {

					if (suicidalMan[j + 1,i].GetComponent<Rock>().GetElement() == currentElementH) {
						Debug.Log("Next tile:" + suicidalMan[i,j+1].GetComponent<Rock>().GetElement() + " Current Tile:"+ currentElementH);
						countHorizontal++;
						Debug.Log("HorizontalCount:" + countHorizontal);
					} else {
						if (countHorizontal >= 2) {
							Debug.Log("HORIZONTAL MATCH of 3 or higher");
							return true; // in future "boolean" return position of runes
						} else {
							Debug.Log("No horizontal match (rip)");
							countHorizontal = 0;
							//currentHorTile = suicidalMan[i,j];
							//currentElementH = currentHorTile.GetComponent<Rock>().GetElement();
						}
					}

					if (suicidalMan[i,j + 1].GetComponent<Rock>().GetElement() == currentElementV) {
						countVertical++;
						Debug.Log("VerticalCount:" + countVertical);
					} else {
						if (countVertical >= 2) {
							Debug.Log("VERTICAL MATCH of 3 or higher");
							return true;
						} else {
							Debug.Log("No vertical match (rip)");
							countVertical = 0;
							//currentVerTile = suicidalMan[i,j];
							//currentElementV = currentVerTile.GetComponent<Rock>().GetElement();
						}
					}
				}

			}
			//first "for" loop is over, "row/column" counter reset
			countHorizontal = 0;
			countVertical = 0;
		}
		return false; //no match found
		
	}
	*/
}