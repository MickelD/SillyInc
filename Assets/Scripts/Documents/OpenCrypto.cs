using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCrypto : DocumentBase
{

    private bool cryptoUnlocked;

    public override void SetDocumentInfo()
    {
        cryptoUnlocked = gameInfo.CryptoDep.unlocked;
        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {
        gameInfo.CryptoDep.unlocked = cryptoUnlocked;
        base.SubmitDocument();
    }

    public void OpenCryptoDept(bool cryptoIsOpen)
    {

        cryptoUnlocked = cryptoIsOpen;

    }

}
