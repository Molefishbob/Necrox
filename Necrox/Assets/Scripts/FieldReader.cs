using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldReader : GameField {

	public bool ReadField () {
		/* fuck this
		int countHorizontal = 0;
		int countVertical = 0;
		string currentElementH = GetGameField()[0,0].GetRockPrefab().element; // What is in the row [0,0], char,string,object? What i'm going to compare?
		string currentElementV = currentElementH;

		// no idea how to call/get 2darray from gamefield "GameObject[,]" properly

		for (int i = 0; i < GameObject.GetLength(0); i++) {
			for (int j = 0; j < GameObject.GetLength(0); j++) {
				
				if (GameObject[i,j] == currentElementH) {
					countHorizontal++;
				} else {
					if (countHorizontal >= 3) {
						return true; // in future "boolean" return position of runes
					} else {
						countHorizontal = 0;
						currentElementH = GameObject[i,j];
					}
				}

				if (GameObject[j,i] == currentElementV) {
					countVertical++;
				} else {
					if (countVertical >= 3) {
						return true;
					} else {
						countVertical = 0;
						currentElementV = GameObject[j,i];
					}
				}

			}
			//first "for" loop is over, "row/column" counter reset
			countHorizontal = 0;
			countVertical = 0;
		}
		*/
		return false; //no match found

	}
}