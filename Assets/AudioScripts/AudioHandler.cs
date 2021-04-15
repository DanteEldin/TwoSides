using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioHandler", menuName = "AudioHandler")]

public class AudioHandler : ScriptableObject 
{
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] Vector2 pitchRange;
    [SerializeField] Vector2 volumeRange;

    [SerializeField] AudioMixerGroup mixerGroup;

    public void PlayAudio()
    {
        float pitchValue = Random.Range(pitchRange.x, pitchRange.y);
        float volumeValue = Random.Range(volumeRange.x, volumeRange.y);

        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Count)];
        GameObject obj = new GameObject("AudioPlayer");
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.AddComponent<AudioRemover>();

        source.pitch = pitchValue;
        source.volume = volumeValue;
        source.clip = audioClip;
        source.outputAudioMixerGroup = mixerGroup;

        source.Play();
    }
}
