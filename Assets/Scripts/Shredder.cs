using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _fadeOutPitch;

    private void OnEnable ()
    {
        GameManager.OnSetShredder += SetShredding;
    }

    private void OnDisable()
    {
        GameManager.OnSetShredder -= SetShredding;
    }

    private void SetShredding(bool set)
    {
        _animator.SetBool("shred", set);

        if (set)
        {
            SoundManager.Instance.sndShredder.PlayAudioCue();
        }
        else
        {
            SoundManager.Instance.sndShredder.StopAudioCueFadeOut(_fadeOutTime, _fadeOutPitch);
        }
    }
}
