using System.Collections.Generic;
using UnityEngine;

namespace TAMK.SpaceShooter.States
{
	/// <summary>
	/// GameStateManager toteuttaa Singleton patterinin. Se takaa, että
	/// tästä luokasta voi olla maksimissaan yksi instanssi kerrallaan luotuna.
	/// </summary>
	public class GameStateManager : MonoBehaviour
	{
		#region Statics

		#region Fields

		/// <summary>
		/// Muuttuja, jossa pidetään tallessa GameStateManager olion ainoata instanssia.
		/// </summary>
		private static GameStateManager _instance;

		#endregion Fields

		#region Properties

		/// <summary>
		/// Tämän propertyn kautta muut luokat pääsevät käsiksi singleton-olioon.
		/// </summary>
		public static GameStateManager Instance
		{
			get
			{
				if ( _instance == null )
				{
					GameObject go = new GameObject( "GameStateManager" );
					_instance = go.AddComponent< GameStateManager >();
				}

				return _instance;
			}
		}

		#endregion Properties

		#endregion Statics

		private List< GameStateBase > _gameStates = new List< GameStateBase >( 4 );

		public GameStateBase CurrentState { get; private set; }

		private void Awake()
		{
			if ( _instance == null )
			{
				_instance = this;
			}
			else if ( _instance != this )
			{
				Destroy( gameObject );
				return;
			}

			DontDestroyOnLoad( gameObject );
			Init();
		}

		private void Init()
		{
			if ( !AddStartingState( new MainMenuState() ) )
			{
				Debug.LogError( "MainMenu state already added." );
			}

			_gameStates.Add( new LevelSelectState());
			_gameStates.Add( new LevelState( GameStateType.Level1, GameStateType.Level2, "Level1" ) );
			_gameStates.Add( new LevelState( GameStateType.Level2, GameStateType.Level3, "Level2" ) );
			_gameStates.Add( new LevelState( GameStateType.Level3, GameStateType.Level4, "Level3" ) );
            _gameStates.Add( new LevelState( GameStateType.Level4, GameStateType.Level5, "Level4"));
            _gameStates.Add(new LevelState(GameStateType.Level5, GameStateType.Level6, "Level5"));
            _gameStates.Add(new LevelState(GameStateType.Level6, GameStateType.Level7, "Level6"));
            _gameStates.Add(new LevelState(GameStateType.Level7, GameStateType.Level8, "Level7"));
            _gameStates.Add(new LevelState(GameStateType.Level8, GameStateType.Level9, "Level8"));
            _gameStates.Add(new LevelState(GameStateType.Level9, GameStateType.Level10, "Level9"));
            _gameStates.Add(new LevelState(GameStateType.Level10, GameStateType.Level11, "Level10"));
            _gameStates.Add(new LevelState(GameStateType.Level11, GameStateType.Level12, "Level11"));
            _gameStates.Add(new LevelState(GameStateType.Level12, GameStateType.Level13, "Level12"));
            _gameStates.Add(new LevelState(GameStateType.Level13, GameStateType.Level14, "Level13"));
            _gameStates.Add(new LevelState(GameStateType.Level14, GameStateType.Level15, "Level14"));
            _gameStates.Add(new LevelState(GameStateType.Level15, GameStateType.Victory, "Level15"));
            _gameStates.Add(new LevelState(GameStateType.Victory, GameStateType.MainMenu, "Victory"));
        }

		private bool AddStartingState( GameStateBase startingState )
		{
			foreach ( var gameState in _gameStates )
			{
				if ( gameState.Type == startingState.Type )
				{
					return false;
				}
			}

			_gameStates.Add( startingState );
			CurrentState = startingState;
			CurrentState.Activate();
			return true;
		}

		public bool ChangeState( GameStateType targetState )
		{
			if ( !CurrentState.IsValidTargetState( targetState ) )
			{
				return false;
			}

			GameStateBase nextState = GetStateByType( targetState );
			if ( nextState == null )
			{
				return false;
			}

			CurrentState.Deactivate();
			CurrentState = nextState;
			CurrentState.Activate();

			return true;
		}

		private GameStateBase GetStateByType( GameStateType type )
		{
			foreach ( GameStateBase gameState in _gameStates )
			{
				if ( gameState.Type == type )
				{
					return gameState;
				}
			}

			return null;
		}
		private void OnDisable() {
			GameManager.SaveSettings();
		}
	}
}