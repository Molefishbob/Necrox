using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    
    public void Pause() {
        FindObjectOfType<GameLogic>()._paused = true;
        FindObjectOfType<CombatUI>()._paused = true;
    }
}
