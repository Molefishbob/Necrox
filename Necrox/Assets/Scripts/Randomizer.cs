using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer {

    private System.Random random = new System.Random();
	private char[] rockArray = new char[] {'W','W','W','W','W',
										   'E','E','E','E','E',
										   'F','F','F','F','F',
                                           'A','A','A','A','A',
                                           'J','J'};

	public char RandomRock() {
        return rockArray[random.Next(0,21)];
	}
}
