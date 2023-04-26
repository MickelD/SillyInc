using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Paper Interaction"), Space(3)]
    public SoundPlayer sndPaperEntry;
    public SoundPlayer sndPaperPickUp;
    public SoundPlayer sndPaperDrop;
    public SoundPlayer sndPaperOpen;

    [Space(5), Header("Document Interactions"), Space(3)]
    public SoundPlayer sndStampIn;
    public SoundPlayer sndErase;
    public SoundPlayer sndEdit;

    [Space(5), Header("Document Resolution"), Space(3)]
    public SoundPlayer sndSubmitDoc;
    public SoundPlayer sndShredder;
}
