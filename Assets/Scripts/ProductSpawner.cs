using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [Header("UI")]
    public GameObject productItemPrefab;
    public RectTransform contentParent;
    public float spacing = 210f;

    [Header("Navigation")]
    [SerializeField] public ProductDatabase productDatabase;
    [SerializeField] public ProductPageController pageController;
    [SerializeField] public PageManager pageManager;
    [SerializeField] public GameObject ProdPage;

    private List<(GameObject obj, ProductType type)> spawnedProducts = new List<(GameObject, ProductType)>();

    private void Start()
    {
        SpawnAllProducts();
    }

    private void SpawnAllProducts()
    {
        List<ProductData> products = productDatabase.GetAllProducts();

        for (int i = 0; i < products.Count; i++)
        {
            ProductData product = products[i];
            GameObject instance = Instantiate(productItemPrefab, contentParent);

            RectTransform rt = instance.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * spacing, 0);

            spawnedProducts.Add((instance, product.type));

            ProductConstructor constructor = instance.GetComponent<ProductConstructor>();
            if (constructor != null)
            {
                constructor.productId = product.id;
                constructor.pageManager = pageManager;
                constructor.pageController = pageController;
                constructor.ProdPage = ProdPage;
                constructor.database = productDatabase;
            }
        }

        UpdateLayout();
    }

    private void UpdateLayout()
    {
        int visibleIndex = 0;
        foreach (var (obj, _) in spawnedProducts)
        {
            if (obj.activeSelf)
            {
                RectTransform rt = obj.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(visibleIndex * spacing, 0);
                visibleIndex++;
            }
        }

        contentParent.sizeDelta = new Vector2(visibleIndex * spacing, contentParent.sizeDelta.y);
    }

    public void ShowAll()
    {
        foreach (var (obj, _) in spawnedProducts)
        {
            obj.SetActive(true);
        }
        UpdateLayout();
    }

    public void FilterByType(ProductType type)
    {
        foreach (var (obj, productType) in spawnedProducts)
        {
            obj.SetActive(productType == type);
        }
        UpdateLayout();
    }

    public void OnClickLaptop() => FilterByType(ProductType.Laptop);
    public void OnClickPC() => FilterByType(ProductType.PC);
    public void OnClickAGD() => FilterByType(ProductType.AGD);
    public void OnClickShowAll() => ShowAll();
}
