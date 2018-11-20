using TAMK.SpaceShooter.States;
using UnityEngine;

namespace TAMK.SpaceShooter.UI
{
	public class MainMenuController : MonoBehaviour
	{
		private const int MaximumVolume = 1;
		private const string FirstBootKey = "firstboot";

		private void Awake() {
			if (!PlayerPrefs.HasKey(FirstBootKey)) {
			GameManager.ChangeSoundVolume(MaximumVolume);
			GameManager.ChangeMusicVolume(MaximumVolume);
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