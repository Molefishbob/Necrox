using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Staattinen luokka, joka pitää kirjaa pelin tilasta. Tästä luokasta ei voi 
/// luoda olioita.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static int _score = 0;
	public float _soundVolume {
		get;
		private set;
	}
	public float _musicVolume {
		get;
		private set;
	}
	public GameObject _soundSlider;
	public GameObject _musicSlider;

	private void Awake() {
		_soundVolume = 1;
		_musicVolume = 1;
	}

	public static int Score
	{
		get { return _score; }
		private set
		{
			// Mathf.Max palauttaa kahdesta tai useammasta arvosta suurimman. Näin ollen
			// _score-muuttujan arvoksi ei koskaan voida asettaa negatiivista lukua.
			_score = Mathf.Max(0, value);
		}
	}

	public static void IncreaseScore( int amount )
	{
		Score += amount;
	}
	
	public void ChangeSoundVolume() {
		_soundVolume = _soundSlider.GetComponent<Slider>().value;
	}
	
	public void ChangeMusicVolume() {
		_musicVolume = _musicSlider.GetComponent<Slider>().value;
	}
}