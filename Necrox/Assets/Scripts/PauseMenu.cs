using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private const string MainMenu = "MainMenu";

    public void ToMainMenu() {
        GameStateManager.Instance.ChangeState(
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , MainMenu));
    }

    public void PlayGame() {
        CombatUI cUI = FindObjectOfType<CombatUI>();
        FindObjectOfType<GameLogic>()._paused = false;
        cUI._paused = false;
        cUI.PauseGame(false);
    }
}
