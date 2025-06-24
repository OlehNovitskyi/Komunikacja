using System.Collections.Generic;
using UnityEngine;

public class ProductDatabase : MonoBehaviour
{
    [SerializeField] private List<ProductData> allProducts;

    private Dictionary<string, ProductData> productDict;

    private void Awake()
    {
        productDict = new Dictionary<string, ProductData>();
        foreach (var product in allProducts)
        {
            if (!string.IsNullOrEmpty(product.id))
                productDict[product.id] = product;
        }
    }

    public ProductData GetProductByID(string id)
    {
        productDict.TryGetValue(id, out var product);
        return product;
    }

    public List<ProductData> GetAllProducts()
    {
        return new List<ProductData>(allProducts);
    }
}
