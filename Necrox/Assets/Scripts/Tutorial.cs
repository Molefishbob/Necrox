using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TAMK.SpaceShooter.States;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject _firstPage;
    private Camera _camera;
    [SerializeField]
    private AudioClip _buttonClick;

    private void Awake() {
        _camera = transform.parent.GetComponent<CombatUI>()._camera;
        FindObjectOfType<GameLogic>()._paused = true;
        FindObjectOfType<CombatUI>()._paused = true;
    }
    
    public void PlayClickAudio() {
			_camera.GetComponent<CameraManager>()
					.PlaySound(_buttonClick,GameManager._soundVolume,usePitchVariance: false);
		}

    public void StartGame() {
        FindObjectOfType<GameLogic>()._paused = false;
        FindObjectOfType<CombatUI>()._paused = false;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        _firstPage.SetActive(true);
    }
}
