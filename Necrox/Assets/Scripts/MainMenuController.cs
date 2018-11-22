using TAMK.SpaceShooter.States;
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
		private AudioClip _audioClip;
        private bool _startGame;
		private float _timer;
		private float _count = 0;

        private void Awake() {
			if (!PlayerPrefs.HasKey(FirstBootKey)) {
			GameManager.ChangeSoundVolume(DefaultSoundVolume);
			GameManager.ChangeMusicVolume(DefaultMusicVolume);
			Debug.Log(GameManager._soundVolume + " " + GameManager._musicVolume);
			PlayerPrefs.SetInt(FirstBootKey,1);
			} else {
				GameManager.LoadSettings();
			}
			_timer = _audioClip.length + 0.1f;
			Debug.Log(_timer);
		}
		private void Update() {
			if (_startGame) {
				Debug.Log(_count);
				if(_timer <= _count) {
				GameStateManager.Instance.ChangeState( GameStateType.Level1 );
				_count = 0;
				}
				_count += Time.deltaTime;
			}
		}
		public void StartGame()
		{
			Debug.Log("startgame");
			_camera.GetComponent<CameraManager>()
					.PlaySound(_audioClip,GameManager._soundVolume,usePitchVariance: false);
			_startGame = true;
		}

		public void QuitGame()
		{
			Debug.Log( "Quitting" );
			Application.Quit();
		}
	}
}