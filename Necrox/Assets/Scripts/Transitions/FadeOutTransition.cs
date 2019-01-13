using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FadeOutTransition : MonoBehaviour {

    public CanvasGroup _text;
    public float _time;
    public Timer _timer;
    private bool _fadingOut;

	// Use this for initialization
	void Start () {
        _text.alpha = 1;
        ResetTimer();
	}

    private void ResetTimer()
    {
        _timer.SetTime(_time);
        _timer.StartTimer();
    }

    // Update is called once per frame
    void Update () {
        if (!LeanTween.isTweening(gameObject) && _timer.IsCompleted && !_fadingOut)
        {
            LeanTween.alphaCanvas(_text, 0, _time);
            ResetTimer();
            _fadingOut = true;
        }
        if (_timer.IsCompleted && _fadingOut)
        {
            Debug.Log("im not feeling so good");
            gameObject.SetActive(false);
        }
	}

    private void OnEnable()
    {
        _fadingOut = false;
        _text.alpha = 1;
        ResetTimer();
    }
}
