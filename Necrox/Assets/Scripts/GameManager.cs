using System;
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
    private const string LatestLevelKey = "latestlevel";

    public static float _soundVolume {
		get;
		private set;
	}
	public static float _musicVolume {
		get;
		private set;
	}
	
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

	public static void SaveLatestLevel(int level) {

		PlayerPrefs.SetInt(LatestLevelKey, level);

	}

	public static int GetLatestLevel() {
		
		return PlayerPrefs.GetInt(LatestLevelKey,1);

	}

    internal static float GetHighScore(string levelKey)
    {
        return PlayerPrefs.GetFloat(levelKey,0);
    }

    internal static void LoadSettings()
    {
        _soundVolume = PlayerPrefs.GetFloat(SoundVolume, DefaultSoundVolume);
		_musicVolume = PlayerPrefs.GetFloat(MusicVolume, DefaultMusicVolume);
    }

    public static void ChangeSoundVolume(float value) {
		_soundVolume = value;
		PlayerPrefs.SetFloat(SoundVolume, _soundVolume);
	}
	
	public static void ChangeMusicVolume(float value) {
		_musicVolume = value;
		PlayerPrefs.SetFloat(MusicVolume, _musicVolume);
	}

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat(SoundVolume, _soundVolume);
		PlayerPrefs.SetFloat(MusicVolume, _musicVolume);
    }
}