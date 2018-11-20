namespace TAMK.SpaceShooter.States
{
	public class VictoryState : GameStateBase
	{
		public override GameStateType Type
		{
			get { return GameStateType.Victory; }
		}
		public override string Scene
		{
			get { return "Victory"; }
		}

		public VictoryState()
		{
			AddTargetState( GameStateType.MainMenu );
			AddTargetState( GameStateType.Level1 );
		}
	}
}