using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _newGameText;
    [SerializeField] TextMeshProUGUI _continueGameText;
    [SerializeField] Button _continueGameButton;
    [SerializeField] EventTrigger _continueEventTrigger;

    private void Start()
    {
        if (FileSaver.savedFile.day <= 1)
        {
            _newGameText.text = _continueGameText.text = "";

            _continueGameButton.interactable = _continueEventTrigger.enabled = false;
        }
        else
        {
            _newGameText.text = "Saved progress will be lost!";
            _continueGameText.text = "Day " + FileSaver.savedFile.day.ToString();

            _continueGameButton.interactable = _continueEventTrigger.enabled = true;
        }
    }

    public void NewGame()
    {
        FileSaver.ClearJson();
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
