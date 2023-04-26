using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlyer : DocumentBase
{
    public override void DocumentDiscarded()
    {
        GameManager.Instance.CreateDocument(GameManager.Instance.DocumentPrefabs.TutorialFlyer);

        base.DocumentDiscarded();
    }
}
