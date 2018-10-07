using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private bool _fallingToPlace;
    private string _element;
    private int _yPosition;

    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (_fallingToPlace) {
            transform.localPosition = new Vector3(0,transform.localPosition.y - 0.125f,0);
        }
        if (transform.localPosition.y <= _yPosition) {
            _fallingToPlace = false;
        }
        

    }

	public void Init(float x, int y, string element) {
        int intX = Mathf.CeilToInt(x);

        //Debug.Log(intX);

        intX = ToArrayValues(intX);

        pos = new int[] {intX,y};

        //Debug.Log(string.Format("X: {0} Y: {1}",pos[0],pos[1]));

        y = ToPosValues(y);

        _yPosition = y;

        this._element = element;
        _fallingToPlace = true;
	}

    private int ToPosValues(int value) {
        switch (value) {
            case 0:
                return 3;
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 0;
            case 4:
                return -1;
            case 5:
                return -2;
            default:
                Debug.Log("Mistakes: Y-pos of tile");
                return 3;
        }
    }

    private int ToArrayValues(int value) {
        switch (value) {
            case 3:
                return 5;
            case 2:
                return 4;
            case 1:
                return 3;
            case 0:
                return 2;
            case -1:
                return 1;
            case -2:
                return 0;
            default:
                Debug.Log("Mistakes: X-pos of tile");
                return 5;

        }
    }

    public void ChangeParent(GameObject column) {

        Debug.Log("Old Parent: " + transform.parent.GetComponent<ColumnBehaviour>().rowNumber +
                  " New Parent: " + column.GetComponent<ColumnBehaviour>().rowNumber);
        Debug.Log("X:"+ pos[0] + " Y:" + pos[1]);

        transform.parent = column.transform;
    }

    public void SetPos(int[] pos) {
        this.pos = pos;
    }

    public int[] GetPos() {
        return pos;
    }
    public string GetElement() {
        return _element;
    }
}