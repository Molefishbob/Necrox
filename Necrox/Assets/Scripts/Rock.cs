using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private float speed = 0.018f;
    private bool onMove;
    private float yValue;

	// Use this for initialization
	void Start () {
        }
	
	// Update is called once per frame
	void Update () {

    }

	public void Init(int x, int y) {
        int[] pos = new int[] {x,y};
		this.pos = pos;
        onMove = true;
	}
}