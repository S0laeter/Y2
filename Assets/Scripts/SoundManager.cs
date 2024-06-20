using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject audioSourceObject;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySoundClip(AudioClip[] soundClip, Transform soundLocation, float volume)
    {
        //take a random sound from the array
        int rand = Random.Range(0, soundClip.Length);

        audioSourceObject.transform.position = soundLocation.position;

        //assign the sound clip and play
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSource.clip = soundClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
    }

}
