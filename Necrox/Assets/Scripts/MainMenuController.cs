using TAMK.SpaceShooter.States;
using UnityEngine;

namespace TAMK.SpaceShooter.UI
{
	public class MainMenuController : MonoBehaviour
	{
		private const float SoundDefaultVolume = 0.4f;
		private const float MusicDefaultVolume = 0.6f;
		private const string FirstBootKey = "firstboot";

		private void Awake() {
			if (!PlayerPrefs.HasKey(FirstBootKey)) {
			GameManager.ChangeSoundVolume(SoundDefaultVolume);
			GameManager.ChangeMusicVolume(MusicDefaultVolume);
			Debug.Log(GameManager._soundVolume + " " + GameManager._musicVolume);
			PlayerPrefs.SetInt(FirstBootKey,1);
		}
		}
		public void StartGame()
		{
			GameStateManager.Instance.ChangeState( GameStateType.Level1 );
		}

		public void QuitGame()
		{
			Debug.Log( "Quitting" );
			Application.Quit();
		}
	}
}