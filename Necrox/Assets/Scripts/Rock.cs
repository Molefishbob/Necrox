using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private bool onMove;
    private string element;
    private int yPosition;

	// Use this for initialization
	void Start () {
        }
	
	// Update is called once per frame
	void Update () {
        if (onMove) {
            transform.localPosition = new Vector3(0,transform.localPosition.y - 0.125f,0);
        }
        if (transform.localPosition.y <= yPosition) {
            onMove = false;
        }
    }

	public void Init(float x, int y, string element) {
        int intX = Mathf.CeilToInt(x);

        Debug.Log(intX);
        switch (intX) {
            case 3:
                intX = 5;
                break;
            case 2:
                intX = 4;
                break;
            case 1:
                intX = 3;
                break;
            case 0:
                intX = 2;
                break;
            case -1:
                intX = 1;
                break;
            case -2:
                intX = 0;
                break;
            default:
                Debug.Log("Mistakes: X-pos of tile");
                break;

        }
        pos = new int[] {intX,y};
        Debug.Log(string.Format("X: {0} Y: {1}",pos[0],pos[1]));
        switch (y) {
            case 0:
                y = 3;
                break;
            case 1:
                y = 2;
                break;
            case 2:
                y = 1;
                break;
            case 3:
                y = 0;
                break;
            case 4:
                y = -1;
                break;
            case 5:
                y = -2;
                break;
            default:
                Debug.Log("Mistakes: Y-pos of tile");
                break;

        }
        yPosition = y;
        this.element = element;
        onMove = true;
	}
}