using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RectTransform SubmissionStartTransform;
    public RectTransform DiscardStartTransform;

    public System.Action OnDiscardHeldDocument;
    public System.Action<bool> OnOpenHeldDocument;
    public System.Action OnTrySubmitHeldDocument;
    public System.Action OnDropDocument;

    public void DiscardHeldDocument()
    {
        OnDiscardHeldDocument?.Invoke();
    }

    public void OpenHeldDocument(bool open)
    {
        OnOpenHeldDocument?.Invoke(open);
    }

    public void TrySubmitHeldDocument()
    {
        OnTrySubmitHeldDocument?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnDropDocument?.Invoke();
        }
    }
}
