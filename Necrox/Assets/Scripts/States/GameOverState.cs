namespace TAMK.SpaceShooter.States
{
	public class GameOverState : GameStateBase
	{
		public override GameStateType Type
		{
			get { return GameStateType.GameOver; }
		}
		public override string Scene
		{
			get { return "GameOver"; }
		}

		public GameOverState()
		{
			AddTargetState( GameStateType.Level1 );
			AddTargetState( GameStateType.MainMenu );
		}
	}
}