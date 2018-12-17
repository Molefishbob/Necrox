using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTransition : MonoBehaviour {

	[SerializeField]
	private float _time;
    private bool _timeToStop;

    // Use this for initialization
    void Awake() {

		transform.localScale = Vector2.zero;
		
	}
	
	// Update is called once per frame
	void Update() {

		if (!_timeToStop) {

			LeanTween.scale(gameObject,Vector2.one,_time);
			_timeToStop = true;

		}
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		transform.localScale = Vector2.zero;
		_timeToStop = false;
	}
}
