using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    
    public void Pause() {
        CombatUI cUI = FindObjectOfType<CombatUI>();

        FindObjectOfType<GameLogic>()._paused = true;
        cUI._paused = true;
        cUI.PauseGame(true);
    }
}
