using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductPageController : MonoBehaviour
{
    [Header("UI Elements in Product Page > Panel")]
    public Image productImage;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI shortDescriptionText;
    public TextMeshProUGUI longDescriptionText;
    public TextMeshProUGUI reviewsText;

    public GameObject productPageRoot;
    public ProductDatabase database;

    public ProductData dataForFuture;

    public void SendDataToProdPage(string productId)
    {
        ProductData data = database.GetProductByID(productId);
        if (data != null)
        {
            productImage.sprite = data.image;
            productNameText.text = data.name;
            priceText.text = $"{data.price:0.00} zł";
            shortDescriptionText.text = data.shortDescription;
            longDescriptionText.text = data.longDescription;
            reviewsText.text = string.Join("\n• ", data.reviews);

            dataForFuture = data;
        }
        else
        {
            Debug.LogWarning($"Product with ID '{productId}' not found in database.");
        }
    }
}