using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDayUI : MonoBehaviour
{
    [SerializeField] GameObject _nextDayButton;

    private void OnEnable()
    {
        GameManager.OnUpdateFulFilledDocuments += CoverImageManagment;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateFulFilledDocuments -= CoverImageManagment;
    }
    private void CoverImageManagment(int filledDocs, int docQueueSize)
    {
        if (filledDocs >=docQueueSize )
        {
            _nextDayButton.SetActive(true);
        }
        else
        {
            _nextDayButton.SetActive(false);
        }
    }

    public void ChageDay()
    {
        GameManager.Instance.EndDay();
    }

}
