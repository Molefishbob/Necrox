using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int[] pos = new int[2];
    private bool onMove;
    private float yValue;
    private float PPU = 32;

	// Use this for initialization
	void Start () {
        }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (onMove) {
            transform.localPosition = new Vector3(0,transform.localPosition.y - 0.04f,0);
        }
    }
/*
    private void LateUpdate() {
		Vector3 position = transform.localPosition;

		position.x = (Mathf.Round(transform.parent.position.x * PPU) / PPU) - transform.parent.position.x; 
		position.y = (Mathf.Round(transform.parent.position.y * PPU) / PPU) - transform.parent.position.y;

		transform.localPosition = position;
    }
*/

	public void Init(int x, int y) {
        int[] pos = new int[] {x,y};
		this.pos = pos;
        Debug.Log(string.Format("X: {0} Y: {1} Z: {2}",transform.localPosition.x,transform.localPosition.y,transform.localPosition.z));
        onMove = true;
	}

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Rock" || other.gameObject.tag == "GameField") {
            //Debug.Log("TRIGGER");
            onMove = false;
        }

    }
}