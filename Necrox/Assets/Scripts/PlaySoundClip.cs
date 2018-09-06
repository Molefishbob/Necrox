using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClip : MonoBehaviour {
    public float pitchVariance;
    private float lifeTime;
    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        if(timer >= lifeTime) {
            Destroy(gameObject);
        }
    }

    public void PlayClip(AudioClip audioClip, float volume, bool usePitchVariance) {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        if(usePitchVariance) audioSource.pitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);
        lifeTime = audioSource.clip.length + 1;
        timer = 0;
        audioSource.Play();
    }
}
