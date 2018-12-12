using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Static class that is used to store data on the device and between scenes.
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
    public static int comboCount { 
		get;
		set;
	}
    public static int defaultMultiplier { 
		get;
		set;
		}

	/// <summary>
	/// Method used after the level is complete to register the score gotten in the level.
	/// Only saves the score if it is higher than the previous highscore in the level.
	/// If there is not a highscore registered it defaults to 0 on the current highscore.
	/// </summary>
	/// <param name="levelKey">The levelkey where to store the data. (ex. Level1)</param>
	/// <param name="score">The players score</param>
	/// <returns>Returns true if the player has made a new highscore.</returns>
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

	/// <summary>
	/// Save the latestlevel the player has entered for Levelselect to use.
	/// </summary>
	/// <param name="level">The level integer</param>
	public static void SaveLatestLevel(int level) {

		PlayerPrefs.SetInt(LatestLevelKey, level);

	}

	/// <summary>
	/// Returns the latestlevel the player has entered.
	/// </summary>
	/// <returns>the latest level integer</returns>
	public static int GetLatestLevel() {
		
		return PlayerPrefs.GetInt(LatestLevelKey,1);

	}

	/// <summary>
	/// Returns the highscore for any levels
	/// </summary>
	/// <param name="levelKey">The levelkey</param>
	/// <returns>The revelant highscore</returns>
    internal static float GetHighScore(string levelKey)
    {
        return PlayerPrefs.GetFloat(levelKey,0);
    }

	/// <summary>
	/// Loads the saved volume settings.
	/// </summary>
    internal static void LoadSettings()
    {
        _soundVolume = PlayerPrefs.GetFloat(SoundVolume, DefaultSoundVolume);
		_musicVolume = PlayerPrefs.GetFloat(MusicVolume, DefaultMusicVolume);
    }

	/// <summary>
	/// Changes the sound volume.
	/// </summary>
	/// <param name="value">The value to which it changes it to</param>
    public static void ChangeSoundVolume(float value) {
		_soundVolume = value;
		PlayerPrefs.SetFloat(SoundVolume, _soundVolume);
	}
	
	/// <summary>
	/// Changes the music volume.
	/// </summary>
	/// <param name="value">The value to which it changes it to</param>
	public static void ChangeMusicVolume(float value) {
		_musicVolume = value;
		PlayerPrefs.SetFloat(MusicVolume, _musicVolume);
	}

	/// <summary>
	/// Saves the current volume settings.
	/// </summary>
    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat(SoundVolume, _soundVolume);
		PlayerPrefs.SetFloat(MusicVolume, _musicVolume);
    }
}