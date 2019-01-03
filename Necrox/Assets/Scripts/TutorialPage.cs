using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPage : MonoBehaviour {

	public GameObject _buttons;

	// Use this for initialization
	void Start () {
		
	}
	private void OnDisable() {
		if (!_buttons.activeSelf) {
			_buttons.SetActive(true);
		}
	}
}
