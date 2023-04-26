using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductActivation : MonoBehaviour
{

    [SerializeField] private RawImage _spriteRenderer;
    [SerializeField] private GameObject _duckSprite;
    [SerializeField] private GameObject _eggSprite;
    [HideInInspector] public ProductSavedData AffectedProduct;

    public void SetUpSpriteRenderer()
    {

        if (AffectedProduct.hasBeenUnlocked && AffectedProduct.isInProduction)
        {
            _eggSprite.SetActive(false);
            SetGrayScale(0);
        }
        else if (AffectedProduct.hasBeenUnlocked && !AffectedProduct.isInProduction)
        {
            _eggSprite.SetActive(false);
            _duckSprite.SetActive(true);
            SetGrayScale(1);
        }
        else if (!AffectedProduct.hasBeenUnlocked)
        {

            _eggSprite.SetActive(true);
            _duckSprite.SetActive(false);
            SetGrayScale(1);
        }

    }


    public void SetGrayScale(float amount)
    {

        _spriteRenderer.material.SetFloat("_GrayscaleAmount", amount);

    }

    public void SetInProduction()
    {

        if (AffectedProduct.hasBeenUnlocked && !AffectedProduct.isInProduction)
        {

            AffectedProduct.isInProduction = true;
            SetUpSpriteRenderer();

        }
        else if (AffectedProduct.hasBeenUnlocked && AffectedProduct.isInProduction)
        {

            AffectedProduct.isInProduction = false;
            SetUpSpriteRenderer();

        }

    }

}
