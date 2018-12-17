using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to manage the volumelevel in settings.
/// </summary>
public class SoundManager : MonoBehaviour {
    private const string MainMenu = "MainMenu";
    [SerializeField]
	private Slider _soundSlider;
	[SerializeField]
	private Slider _musicSlider;
	private AudioSource _audioSource;
	[SerializeField]
	private Camera _camera;

	/// <summary>
	/// Gets the saved values for the sliders.
	/// </summary>
	private void Awake() {
		
		if ( SceneManager.GetActiveScene().name == MainMenu) {

			_audioSource = _camera.GetComponent<AudioSource>();

		} else {

			_audioSource = transform.parent.parent.GetComponent<CombatUI>()._camera.GetComponent<AudioSource>();

		}
		
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
