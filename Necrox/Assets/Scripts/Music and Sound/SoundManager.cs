using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to manage the volumelevel in settings.
/// </summary>
public class SoundManager : MonoBehaviour {

	[SerializeField]
	private Slider _soundSlider;
	[SerializeField]
	private Slider _musicSlider;
	[SerializeField]
	private AudioSource _audioSource;

	/// <summary>
	/// Gets the saved values for the sliders.
	/// </summary>
	private void Awake() {
		_soundSlider.value = GameManager._soundVolume;
		_musicSlider.value = GameManager._musicVolume;
	}

	/// <summary>
	/// Changes the music volume.
	/// </summary>
	public void ChangeMusicVolume() {
		GameManager.ChangeMusicVolume(_musicSlider.value);
		_audioSource.volume = _musicSlider.value;
	}

	/// <summary>
	/// Changes the sound volume.
	/// </summary>
	public void ChangeSoundVolume() {
		GameManager.ChangeSoundVolume(_soundSlider.value);
	}

	/// <summary>
	/// Saves the current settings.
	/// </summary>
	public void SaveSettings() {
		GameManager.SaveSettings();
	}
}
