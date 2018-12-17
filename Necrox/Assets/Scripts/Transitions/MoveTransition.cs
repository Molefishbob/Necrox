using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : MonoBehaviour {

	[SerializeField]
	private float _time;
	[SerializeField]
	private float _endPosition;
	[SerializeField]
	private float _startPosition;
    private bool _timeToStop;

    // Use this for initialization
    void Awake() {

		transform.localPosition = new Vector2(_startPosition,transform.localPosition.y);
		
	}
	
	// Update is called once per frame
	void Update() {

		if (!_timeToStop) {

			LeanTween.moveLocalX(gameObject,_endPosition,_time);
			_timeToStop = true;

		}
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		transform.localPosition = new Vector2(_startPosition,transform.localPosition.y);
		_timeToStop = false;
	}
}
