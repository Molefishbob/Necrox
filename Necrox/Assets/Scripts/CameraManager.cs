using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera _camera;
	[SerializeField]
	private PlaySoundClip _soundPlayer;
	private float DesignOrthographicSize;
    private float DesignAspect;
    private float DesignWidth;

	public float DesignAspectHeight;
    public float DesignAspectWidth;

	// Use this for initialization
	void Start () {

		_camera = gameObject.GetComponent<Camera>();
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.volume = GameManager._musicVolume;

		DesignOrthographicSize = _camera.orthographicSize;
        DesignAspect = DesignAspectHeight / DesignAspectWidth;
        DesignWidth = DesignOrthographicSize * DesignAspect;

        Resize();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Resize()
    {       
        float wantedSize = DesignWidth / _camera.aspect;
        _camera.orthographicSize = Mathf.Max(wantedSize, 
            DesignOrthographicSize);
    }

    internal void PlaySound(AudioClip audioClip, float soundVolume, bool usePitchVariance)
    {

        Instantiate(_soundPlayer,transform.position,Quaternion.identity,transform).PlayClip(audioClip,soundVolume,usePitchVariance);

    }
}