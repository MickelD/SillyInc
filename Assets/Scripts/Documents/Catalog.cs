using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catalog : DocumentBase
{


    [Header("Products"), Space(3)]
    [SerializeField] private ProductActivation _basicDuck;
    [SerializeField] private ProductActivation _conducktor;
    [SerializeField] private ProductActivation _duckabel;
    [SerializeField] private ProductActivation _duckguard;
    [SerializeField] private ProductActivation _duckNorris;
    [SerializeField] private ProductActivation _ducktor;
    [SerializeField] private ProductActivation _dulk;
    [SerializeField] private ProductActivation _duckOfLiberty;
    [SerializeField] private ProductActivation _gentleduck;

    public override void SetDocumentInfo()
    {

        _basicDuck.AffectedProduct = gameInfo.basicDuck;
        _basicDuck.SetUpSpriteRenderer();

        _conducktor.AffectedProduct = gameInfo.conducktor;
        _conducktor.SetUpSpriteRenderer();

        _duckabel.AffectedProduct = gameInfo.duckabel;
        _duckabel.SetUpSpriteRenderer();

        _duckguard.AffectedProduct = gameInfo.duckguard;
        _duckguard.SetUpSpriteRenderer();

        _duckNorris.AffectedProduct = gameInfo.duckNorris;
        _duckNorris.SetUpSpriteRenderer();

        _ducktor.AffectedProduct = gameInfo.ducktor;
        _ducktor.SetUpSpriteRenderer();

        _dulk.AffectedProduct = gameInfo.dulk;
        _dulk.SetUpSpriteRenderer();

        _duckOfLiberty.AffectedProduct = gameInfo.duckOfLiberty;
        _duckOfLiberty.SetUpSpriteRenderer();

        _gentleduck.AffectedProduct = gameInfo.gentleduck;
        _gentleduck.SetUpSpriteRenderer();

        base.SetDocumentInfo();
    }

    public override void SubmitDocument()
    {
        
        base.SubmitDocument();
    }

    



}
