using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartManager : MonoBehaviour
{
    public static CartManager Instance { get; private set; }
    public ProductPageController productPage;
    public ProductDatabase productDatabse;

    [Header("Cart UI")]
    public GameObject cartContentParent;
    public GameObject cartItemPrefab;

    [Header("Final page UI")]
    public GameObject FinalContentParent;
    public GameObject FinalItemPrefab;

    private List<string> productIDs = new List<string>();

    public void AddOnClickFromProductPage()
    {
        if (productPage != null)
        {
            if (!string.IsNullOrEmpty(productPage.dataForFuture.id)) {
                productIDs.Add(productPage.dataForFuture.id);
                Debug.Log($"Product added to cart! {productIDs}");
            }
        }
    }
    public void AddToCart(string id)
    {
        if (!string.IsNullOrEmpty(id))
            productIDs.Add(id);
    }
    public List<ProductData> GetCartItems()
    {
        var result = new List<ProductData>();
        foreach (var id in productIDs)
        {
            var data = productDatabse.GetProductByID(id);
            if (data != null)
                result.Add(data);
        }
        return result;
    }
    public void ClearCart()
    {
        productIDs.Clear();
        foreach (Transform child in cartContentParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RefreshCartUI()
    {
        foreach (Transform child in cartContentParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i<productIDs.Count; i++)
        {
            var id = productIDs[i];
            var data = productDatabse.GetProductByID(id);
            if (data != null)
            {
                GameObject item = Instantiate(cartItemPrefab, cartContentParent.transform);

                RectTransform rt = item.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(i * 200f, 0);

                Image img = item.transform.Find("Image").GetComponent<Image>();
                TextMeshProUGUI name = item.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI price = item.transform.Find("Price").GetComponent<TextMeshProUGUI>();
                Button removeBtn = item.transform.Find("RemoveButton").GetComponent<Button>();

                img.sprite = data.image;
                name.text = data.name;
                price.text = $"{data.price:0.00} zł";

                removeBtn.onClick.AddListener(() =>
                {
                    productIDs.Remove(id);
                    RefreshCartUI();
                });
            }
        }
    }

    public void RefreshFinalUI()
    {
        foreach (Transform child in FinalContentParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < productIDs.Count; i++)
        {
            var id = productIDs[i];
            var data = productDatabse.GetProductByID(id);
            if (data != null)
            {
                GameObject item = Instantiate(FinalItemPrefab, FinalContentParent.transform);

                RectTransform rt = item.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(i * 200f, 0);

                Image img = item.transform.Find("Image").GetComponent<Image>();
                TextMeshProUGUI name = item.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI price = item.transform.Find("Price").GetComponent<TextMeshProUGUI>();
                
                img.sprite = data.image;
                name.text = data.name;
                price.text = $"{data.price:0.00} zł";
            }
        }
    }
}
