using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField] private Animator _pauseAnimation;
    [SerializeField] private string _pauseAnimatorTag;

    private bool _isPaused;

    private void Start()
    {
        SetPause(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            SetPause(!_isPaused);
        }
    }

    public void SetPause(bool pause)
    {
        _isPaused = pause;

        Time.timeScale = pause? 0f : 1f;

        _pauseAnimation.SetBool(_pauseAnimatorTag, pause);

        SoundManager.Instance.sndPaperOpen.PlayAudioCue();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
