﻿using TAMK.SpaceShooter.States;
using UnityEngine;

namespace TAMK.SpaceShooter.UI
{
	public class MainMenuController : MonoBehaviour
	{
		private const float DefaultSoundVolume = 0.6f;
		private const float DefaultMusicVolume = 0.4f;
		private const string FirstBootKey = "firstboot";
		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private AudioClip _buttonClick;
		[SerializeField]
		private GameObject _gameManager;
        private bool _startGame;
		private float _timer;
		private float _count = 0;
        private bool _quitGame;

        private void Awake() {
			if (!PlayerPrefs.HasKey(FirstBootKey)) {
			GameManager.ChangeSoundVolume(DefaultSoundVolume);
			GameManager.ChangeMusicVolume(DefaultMusicVolume);
			PlayerPrefs.SetInt(FirstBootKey,1);
			} else {
				GameManager.LoadSettings();
			}
			_timer = _buttonClick.length;
			DontDestroyOnLoad(_gameManager);
		}
		private void Update() {
			if (_startGame) {
				if(_timer <= _count) {
				GameStateManager.Instance.ChangeState( GameStateType.Level1 );
				_count = 0;
				}
				_count += Time.deltaTime;
			} else if (_quitGame) {
				if(_timer <= _count) {
				GameStateManager.Instance.ChangeState( GameStateType.Level1 );
				_count = 0;
				}
			}
		}
		public void StartGame()
		{
			Debug.Log("startgame");
			_camera.GetComponent<CameraManager>()
					.PlaySound(_buttonClick,GameManager._soundVolume,usePitchVariance: false);
			_startGame = true;
		}

		public void Settings() {
			_camera.GetComponent<CameraManager>()
					.PlaySound(_buttonClick,GameManager._soundVolume,usePitchVariance: false);
		}

		public void QuitGame()
		{
			Debug.Log( "Quitting" );
			_camera.GetComponent<CameraManager>()
					.PlaySound(_buttonClick,GameManager._soundVolume,usePitchVariance: false);
			_quitGame = true;
			Application.Quit();
		}
	}
}