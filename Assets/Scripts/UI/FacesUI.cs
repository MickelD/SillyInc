using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacesUI : MonoBehaviour
{
    [SerializeField] RawImage Cara100;
    [SerializeField] RawImage Cara75;
    [SerializeField] RawImage Cara55;
    [SerializeField] RawImage Cara45;
    [SerializeField] RawImage Cara25;
    private float reputation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reputation = GameManager.Instance._currentSave.reputation;
        FaceManagment();
    }

    private void FaceManagment()
    {
        if (reputation >= 0.75)
        {
            Cara100.enabled = true;
            Cara75.enabled = false;
            Cara55.enabled = false;
            Cara45.enabled = false;
            Cara25.enabled = false;
        }
        else if (reputation >= 0.55)
        {
            Cara100.enabled = false;
            Cara75.enabled = true;
            Cara55.enabled = false;
            Cara45.enabled = false;
            Cara25.enabled = false;
        }
        else if (reputation >= 0.45)
        {
            Cara100.enabled = false;
            Cara75.enabled = false;
            Cara55.enabled = true;
            Cara45.enabled = false;
            Cara25.enabled = false;
        }
        else if (reputation >= 0.25)
        {
            Cara100.enabled = false;
            Cara75.enabled = false;
            Cara55.enabled = false;
            Cara45.enabled = true;
            Cara25.enabled = false;
        }
        else if (reputation >= 0)
        {
            Cara100.enabled = false;
            Cara75.enabled = false;
            Cara55.enabled = false;
            Cara45.enabled = false;
            Cara25.enabled = true;
        }
    }
}
