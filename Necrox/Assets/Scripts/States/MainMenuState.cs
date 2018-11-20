namespace TAMK.SpaceShooter.States
{
	public class MainMenuState : GameStateBase
	{
		public override GameStateType Type
		{
			get { return GameStateType.MainMenu; }
		}
		public override string Scene
		{
			get { return "MainMenu"; }
		}

		public MainMenuState()
		{
			AddTargetState( GameStateType.Level1 );
		}
	}
}