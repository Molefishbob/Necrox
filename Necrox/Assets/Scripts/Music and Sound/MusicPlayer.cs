using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour {

	private AudioSource _audioPlayer;

	// Use this for initialization
	void Start () {
		_audioPlayer = GetComponent<AudioSource>();
		_audioPlayer.Play();
	}
}
