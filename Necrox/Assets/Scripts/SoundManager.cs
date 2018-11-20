using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	[SerializeField]
	private Slider _soundSlider;
	[SerializeField]
	private Slider _musicSlider;

	public void ChangeMusicVolume() {
		GameManager.ChangeMusicVolume(_musicSlider.value);
	}
	public void ChangeSoundVolume() {
		GameManager.ChangeSoundVolume(_soundSlider.value);
	}
}
