using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private bool onMove;
    private float yValue;
    private float PPU = 32;
    private string element;
    private int yPosition;

	// Use this for initialization
	void Start () {
        }
	
	// Update is called once per frame
	void Update () {
        if (onMove) {
            transform.localPosition = new Vector3(0,transform.localPosition.y - 0.20f,0);
        }
        if (transform.localPosition.y <= yPosition) {
            onMove = false;
        }
    }

	public void Init(int x, int y, string element) {
        int[] pos = new int[] {x,y};
        Debug.Log(transform.localPosition.y);
        
        switch (y) {
            case 0:
                Debug.Log("first row");
                y = 3;
                break;
            case 1:
                Debug.Log("second row");
                y = 2;
                break;
            case 2:
                Debug.Log("third row");
                y = 1;
                break;
            case 3:
                Debug.Log("fourth row");
                y = 0;
                break;
            case 4:
                Debug.Log("fifth row");
                y = -1;
                break;
            case 5:
                Debug.Log("sixth row");
                y = -2;
                break;
            default:
                Debug.Log("Mistakes: Y-pos of tile");
                break;

        }

		this.pos = pos;
        yPosition = y;
        this.element = element;
        onMove = true;
	}
}