using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera _camera;

	// Use this for initialization
	void Start () {
		_camera = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (System.Math.Round(_camera.aspect,2) == (float) 9/18) {
			_camera.orthographicSize  = 6.7f;
		}
		if (System.Math.Round(_camera.aspect,2) == System.Math.Round((float) 9/16,2)) {
			_camera.orthographicSize  = 6;
		}
	}
}
