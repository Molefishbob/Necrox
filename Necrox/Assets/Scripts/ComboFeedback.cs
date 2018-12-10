using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboFeedback : MonoBehaviour {
    public bool comboOver { 
		get;
		set;
		}
    public int comboCount { 
		get; 
		set;
		}

    private const float EndPosition = 100f;
	private const float MoveTime = 1f;
    private const float Time = 1f;
    public TMP_Text _goodJob;
	public TMP_Text _amazing;
	public TMP_Text _incredible;
	public TMP_Text _unbelievable;
	public TMP_Text _comboMaster;
	private Timer _goodjobTimer;
	private Timer _amazingTimer;
	private Timer _incredibleTimer;
	private Timer _unbelievableTimer;
	private Timer _combomasterTimer;
    private float _lifeTime = 2f;

    // Use this for initialization
    void Start () {
		_goodjobTimer = GetComponent<Timer>();
		_amazingTimer = GetComponent<Timer>();
		_incredibleTimer = GetComponent<Timer>();
		_unbelievableTimer = GetComponent<Timer>();
		_combomasterTimer = GetComponent<Timer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (comboOver)
        {
            GetComboText();
        }
        CheckForTextMovement();
    }

    private void CheckForTextMovement()
    {
        if (!LeanTween.isTweening(_goodJob.gameObject) && _goodjobTimer.IsCompleted)
        {

            _goodJob.gameObject.SetActive(false);
            _goodJob.rectTransform.localPosition = Vector2.zero;
			_unbelievable.rectTransform.localScale = Vector2.one;

        }

        if (!LeanTween.isTweening(_amazing.gameObject) && _amazingTimer.IsCompleted)
        {

            _amazing.gameObject.SetActive(false);
            _amazing.rectTransform.localPosition = Vector2.zero;
			_unbelievable.rectTransform.localScale = Vector2.one;

        }
        if (!LeanTween.isTweening(_incredible.gameObject) && _incredibleTimer.IsCompleted)
        {

            _incredible.gameObject.SetActive(false);
            _incredible.rectTransform.localPosition = Vector2.zero;
			_unbelievable.rectTransform.localScale = Vector2.one;

        }
        if (!LeanTween.isTweening(_unbelievable.gameObject) && _unbelievableTimer.IsCompleted)
        {

            _unbelievable.gameObject.SetActive(false);
            _unbelievable.rectTransform.localPosition = Vector2.zero;
			_unbelievable.rectTransform.localScale = Vector2.one;

        }
        if (!LeanTween.isTweening(_comboMaster.gameObject) && _combomasterTimer.IsCompleted)
        {

            _comboMaster.gameObject.SetActive(false);
            _comboMaster.rectTransform.localPosition = Vector2.zero;
			_unbelievable.rectTransform.localScale = Vector2.one;

        }
    }

    private void GetComboText()
    {
        if (comboCount < 4 && comboCount > 2 && !_goodJob.gameObject.activeSelf)
        {

            ResetTimer(_goodjobTimer);
            _goodJob.gameObject.SetActive(true);
            LeanTween.scale(_goodJob.gameObject, Vector2.one, Time);
            LeanTween.moveLocalY(_goodJob.gameObject, EndPosition, MoveTime);

        }
        else if (comboCount < 6 && comboCount >= 4 && !_amazing.gameObject.activeSelf)
        {

            ResetTimer(_amazingTimer);
            _amazing.gameObject.SetActive(true);
            LeanTween.scale(_amazing.gameObject, Vector2.one, Time);
            LeanTween.moveLocalY(_amazing.gameObject, EndPosition, MoveTime);

        }
        else if (comboCount < 8 && comboCount >= 6 && !_incredible.gameObject.activeSelf)
        {

            ResetTimer(_incredibleTimer);
            _incredible.gameObject.SetActive(true);
            LeanTween.scale(_incredible.gameObject, Vector2.one, Time);
            LeanTween.moveLocalY(_incredible.gameObject, EndPosition, MoveTime);

        }
        else if (comboCount < 10 && comboCount >= 8 && !_unbelievable.gameObject.activeSelf)
        {

            ResetTimer(_unbelievableTimer);
            _unbelievable.gameObject.SetActive(true);
            LeanTween.scale(_unbelievable.gameObject, Vector2.one, Time);
            LeanTween.moveLocalY(_unbelievable.gameObject, EndPosition, MoveTime);

        }
        else if (comboCount >= 10 && !_comboMaster.gameObject.activeSelf)
        {

            ResetTimer(_combomasterTimer);
            _comboMaster.gameObject.SetActive(true);
            LeanTween.scale(_comboMaster.gameObject, Vector2.one, Time);
            LeanTween.moveLocalY(_comboMaster.gameObject, EndPosition, MoveTime);

        }

        comboOver = false;
    }

    private void ResetTimer(Timer timer) {
		timer.Stop();
		timer.SetTime(_lifeTime);
		timer.StartTimer();
	}
}
