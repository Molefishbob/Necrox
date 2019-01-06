using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAMK.SpaceShooter.States
{
	/// <summary>
	/// Kantaluokka kaikille pelin tiloille
	/// </summary>

	/// Pelin eri tiloja kuvaava tyyppi
	public enum GameStateType
	{
		Error = -1,
		None = 0,
		MainMenu,
		LevelSelect,
		Level1,
		Level2,
		Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Victory
	}

	public abstract class GameStateBase
	{
		public abstract GameStateType Type { get; }
		// Tilaan liittyvän Unity-scenen nimi
		public abstract string Scene { get; }

		private readonly List<GameStateType> _validTargetStates;

		protected GameStateBase()
		{
			_validTargetStates = new List<GameStateType>();
		}
		
		public virtual void Activate()
		{
			if (SceneManager.GetActiveScene().name.ToLower() != Scene.ToLower())
			{
				SceneManager.LoadScene(Scene);
			}
		}

		public virtual void Deactivate()
		{
		}

		public bool IsValidTargetState(GameStateType target)
		{
			return _validTargetStates.Contains(target);
		}

		protected void AddTargetState(GameStateType targetStateType)
		{
			if (!_validTargetStates.Contains(targetStateType))
			{
				_validTargetStates.Add(targetStateType);
			}
		}

		protected bool RemoveTargetState(GameStateType targetStateType)
		{
			return _validTargetStates.Remove(targetStateType);
		}
	}
}