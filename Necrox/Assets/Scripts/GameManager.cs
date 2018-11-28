﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Staattinen luokka, joka pitää kirjaa pelin tilasta. Tästä luokasta ei voi 
/// luoda olioita.
/// </summary>
public static class GameManager
{
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";
    private const float DefaultMusicVolume = 0.4f;
    private const float DefaultSoundVolume = 0.6f;

    public static float _soundVolume {
		get;
		private set;
	}
	public static float _musicVolume {
		get;
		private set;
	}

	// public static int Score
	// {
	// 	get { return _score; }
	// 	private set
	// 	{
	// 		// Mathf.Max palauttaa kahdesta tai useammasta arvosta suurimman. Näin ollen
	// 		// _score-muuttujan arvoksi ei koskaan voida asettaa negatiivista lukua.
	// 		_score = Mathf.Max(0, value);
	// 	}
	// }
	public static bool LevelEnd(string levelKey, float score) {
		float previousScore = PlayerPrefs.GetFloat(levelKey,0);
			if(previousScore ==  0) {
				PlayerPrefs.SetFloat(levelKey,score);
				return true;
			} else {
				if(score > previousScore) {
					PlayerPrefs.SetFloat(levelKey,score);
					return true;
				}
			}
			return false;
		}

    internal static void LoadSettings()
    {
        _soundVolume = PlayerPrefs.GetFloat(SoundVolume, DefaultSoundVolume);
		_musicVolume = PlayerPrefs.GetFloat(MusicVolume, DefaultMusicVolume);
    }

    public static void ChangeSoundVolume(float value) {
		_soundVolume = value;
		PlayerPrefs.SetFloat(SoundKey, _soundVolume);
	}
	
	public static void ChangeMusicVolume(float value) {
		_musicVolume = value;
		PlayerPrefs.SetFloat(MusicKey, _musicVolume);
	}

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat(SoundVolume, _soundVolume);
		PlayerPrefs.SetFloat(MusicVolume, _musicVolume);
    }
}