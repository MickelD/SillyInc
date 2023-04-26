using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioCue[] _audioCues = new AudioCue[1];

    public void PlayAudioCue(bool interrupt = true)
    {
        if (interrupt || (!interrupt && !_audioSource.isPlaying))
        {
            AudioCue chosenCue = _audioCues[Random.Range(1, _audioCues.Length)];

            _audioSource.clip = chosenCue.audioClip;
            _audioSource.volume = chosenCue.volume;
            _audioSource.pitch = chosenCue.pitch;
            _audioSource.loop = chosenCue.loop;

            _audioSource.Play();
        }
    }

    public void StopAudioCueInstant()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    public void StopAudioCueFadeOut(float dur, float targetFinalPitch = 1f)
    {
        if (_audioSource.isPlaying)
        {
            StartCoroutine(Fade(dur, 0f, targetFinalPitch));
        }
    }

    private IEnumerator Fade(float dur, float targetVolume, float targetPitch = 1f)
    {
        float startVol = _audioSource.volume;
        float startPitch = _audioSource.pitch;

        float elapsedTime = 0;

        while (elapsedTime < dur)
        {
            _audioSource.volume = Mathf.Lerp(startVol, targetVolume, (elapsedTime / dur));
            _audioSource.pitch = Mathf.Lerp(startPitch, targetPitch, (elapsedTime / dur));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _audioSource.Stop();
    }
}

[System.Serializable]
public class AudioCue
{
    public AudioClip audioClip;
    public float volume = 1f;
    public float pitch = 1f;
    public bool loop = false;
}