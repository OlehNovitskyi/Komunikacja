using UnityEngine;

public enum ProductType
{
    Laptop,
    PC,
    AGD,
}

[System.Serializable]
public class ProductData
{
    public string id;
    public ProductType type;
    public Sprite image;
    public string name;
    public float price;
    [TextArea(3, 10)]
    public string shortDescription;
    [TextArea(5, 15)]
    public string longDescription;
    public string[] reviews;
}

