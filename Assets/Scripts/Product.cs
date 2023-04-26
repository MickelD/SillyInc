using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class Product : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public string description;
    public int id;
}
