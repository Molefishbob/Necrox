using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera _camera;
	[SerializeField]
	private PlaySoundClip _soundPlayer;

	// Use this for initialization
	void Start () {

		_camera = gameObject.GetComponent<Camera>();
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.volume = GameManager._musicVolume;
		
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

    internal void PlaySound(AudioClip audioClip, float soundVolume, bool usePitchVariance)
    {

        Instantiate(_soundPlayer,transform.position,Quaternion.identity,transform).PlayClip(audioClip,soundVolume,usePitchVariance);

    }
}