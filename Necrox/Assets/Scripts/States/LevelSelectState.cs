namespace TAMK.SpaceShooter.States
{
	public class LevelSelectState : GameStateBase
	{
		public override GameStateType Type
		{
			get { return GameStateType.LevelSelect; }
		}
		public override string Scene
		{
			get { return "LevelSelect"; }
		}

		public LevelSelectState()
		{
			AddTargetState( GameStateType.MainMenu );
			AddTargetState( GameStateType.Level1 );
			AddTargetState( GameStateType.Level2 );
			AddTargetState( GameStateType.Level3 );
			AddTargetState( GameStateType.Level4 );
			AddTargetState( GameStateType.Level5 );
			AddTargetState( GameStateType.Level6 );
			AddTargetState( GameStateType.Level7 );
			AddTargetState( GameStateType.Level8 );
			AddTargetState( GameStateType.Level9 );
			AddTargetState( GameStateType.Level10 );
			AddTargetState( GameStateType.Level11 );
			AddTargetState( GameStateType.Level12 );
			AddTargetState( GameStateType.Level13 );
			AddTargetState( GameStateType.Level14 );
			AddTargetState( GameStateType.Level15 );
		}
	}
}