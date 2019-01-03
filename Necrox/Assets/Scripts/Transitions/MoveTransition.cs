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
	[SerializeField, Tooltip("Not required")]
	private float _secondPosition;
    private bool _timeToStop;
    private bool _movingAgain;

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
		if (_movingAgain) {
			if (!LeanTween.isTweening(gameObject) && transform.position.x != 0) {
				transform.parent.gameObject.SetActive(false);
			}
		}
	}

	public void MoveXAgain() {
		LeanTween.moveLocalX(gameObject,_secondPosition,_time);
		_movingAgain = true;
	}
	public void MoveXNegativeAgain() {
		LeanTween.moveLocalX(gameObject,-_secondPosition,_time);
		_movingAgain = true;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		_movingAgain = false;
		_timeToStop = false;
		if (_secondPosition == _endPosition) {
				transform.localPosition = new Vector2(_startPosition,transform.localPosition.y);
			}
		if (transform.position.x == _secondPosition) {
			transform.position = new Vector2(_startPosition,transform.position.y);
		}
	}
}
