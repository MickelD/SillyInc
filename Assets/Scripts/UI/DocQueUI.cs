using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocQueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _queueText;

    private void OnEnable()
    {
        GameManager.OnUpdateFulFilledDocuments += ChangeQueue;
    }


    private void OnDisable()
    {
        GameManager.OnUpdateFulFilledDocuments -= ChangeQueue;
    }

    private void ChangeQueue(int curr, int max)
    {
        _queueText.text = curr.ToString() + "/" +  max.ToString();
    }
}
