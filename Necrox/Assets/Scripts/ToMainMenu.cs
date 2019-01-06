using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class ToMainMenu : MonoBehaviour {

    private const string MainMenu = "MainMenu";

    public void MainMenuBtn() {
        GameStateManager.Instance.ChangeState(
                        (GameStateType)GameStateType.Parse(typeof(GameStateType)
                        , MainMenu));
    }
}
