using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductConstructor : MonoBehaviour
{
    [Header("Product Metadata")]
    [SerializeField] public string productId;

    [Header("UI References")]
    [SerializeField] public Image imageTarget;
    [SerializeField] public TextMeshProUGUI priceText;
    [SerializeField] public TextMeshProUGUI nameText;

    [Header("Navigation")]
    [SerializeField] public ProductPageController pageController;
    [SerializeField] public PageManager pageManager;
    [SerializeField] public GameObject ProdPage;

    [Header("Data Source")]
    [SerializeField] public ProductDatabase database;

    private void Start()
    {
        if (database == null)
        {
            Debug.LogError("ProductDatabase reference is missing in ProductConstructor.");
            return;
        }
        ProductData product = database.GetProductByID(productId);
        if (product == null)
        {
            Debug.LogWarning($"Product ID '{productId}' not found in database.");
            return;
        }

        if (imageTarget != null && product.image != null)
            imageTarget.sprite = product.image;

        if (priceText != null)
            priceText.text = $"{product.price:0.00} zł";

        if (nameText != null)
            nameText.text = product.name;
    }
    public void OnClick()
    {
        if (pageController != null && pageManager != null)
        {
            pageController.SendDataToProdPage(productId);
            pageManager.ShowPage(ProdPage);
        }
        else
        {
            Debug.LogWarning("ProductPageController or PageManager is not assigned!");
        }
    }


}