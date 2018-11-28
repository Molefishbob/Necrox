namespace TAMK.SpaceShooter.States
{
	public class LevelState : GameStateBase
	{
		private GameStateType _type;
		private string _scene;

		public override GameStateType Type
		{
			get { return _type; }
		}
		public override string Scene
		{
			get { return _scene; }
		}

		public LevelState( GameStateType type, GameStateType nextType, string sceneName )
		{
			_scene = sceneName;
			_type = type;

			AddTargetState( GameStateType.Victory );
			AddTargetState( GameStateType.GameOver );
			AddTargetState( GameStateType.MainMenu );
			if ( nextType != GameStateType.None && nextType != GameStateType.Error )
			{
				AddTargetState( nextType );
			}
		}

		public override void Deactivate()
		{
			base.Deactivate();
			//GameManager.Reset();
		}
	}
}