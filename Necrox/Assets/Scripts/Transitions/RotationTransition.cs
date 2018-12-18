using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransition : MonoBehaviour {

	[SerializeField]
	private float _time;
	[SerializeField]
	private float _endRotation;
	[SerializeField]
	private float _startRotation;
	[SerializeField, Tooltip("Not required")]
	private float _secondRotation;
    private bool _timeToStop;
    private bool _movingAgain;

    // Use this for initialization
    void Awake() {

		transform.eulerAngles = new Vector3(_startRotation,0,0);
		
	}
	
	// Update is called once per frame
	void Update() {

		if (!_timeToStop) {

			LeanTween.rotateLocal(gameObject,new Vector3(_endRotation,0,0),_time);
			_timeToStop = true;

		}
		if (_movingAgain) {
			if (!LeanTween.isTweening(gameObject)) {
				transform.parent.gameObject.SetActive(false);
			}
		}
	}

	public void MoveXAgain() {
		LeanTween.rotateLocal(gameObject,new Vector3(_secondRotation,0,0),_time);
		_movingAgain = true;
	}
	public void MoveXNegativeAgain() {
		LeanTween.rotateLocal(gameObject,new Vector3(-_secondRotation,0,0),_time);
		_movingAgain = true;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		_movingAgain = false;
		transform.localRotation = new Quaternion(_startRotation,0,0,0);
		_timeToStop = false;
	}
}
