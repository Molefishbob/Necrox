using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeDamage : MonoBehaviour {

    public GameObject _damageObject;
    public TMP_Text _damageText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("l")) {
            Debug.Log("you got here");
            StartCoroutine(DamageFlash());
        }
	}
    public void StartFlash() {
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash() {
        for (int n = 0; n < 3; n++) {
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.08f);
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.08f);
        }
        GetComponent<Renderer>().enabled = true;
    }

    internal void ShowDamageText(float temp)
    {
        _damageText.text = "-" + temp.ToString();
        _damageObject.SetActive(true);
    }
}
