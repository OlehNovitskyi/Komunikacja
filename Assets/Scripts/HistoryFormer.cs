using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HistoryFormer : MonoBehaviour
{
    [Header("UI References")]
    public Transform historyContentParent; 
    public GameObject orderHistoryPrefab;
    public float verticalSpacing = 150f;

    public void AddOrderToHistory(List<ProductData> orderedItems, float totalPrice, string deliveryInfo)
    {
        GameObject historyEntry = Instantiate(orderHistoryPrefab, historyContentParent);

        RectTransform rt = historyEntry.GetComponent<RectTransform>();
        int index = historyContentParent.childCount - 1;
        rt.anchoredPosition = new Vector2(0, -index * verticalSpacing);

        TextMeshProUGUI totalPriceTMP = historyEntry.transform.Find("TotalPrice(TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemNamesTMP = historyEntry.transform.Find("ItemNames(TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI deliveryTMP = historyEntry.transform.Find("InfoDelivery(TMP)").GetComponent<TextMeshProUGUI>();

        totalPriceTMP.text = $"Total: {totalPrice:0.00} zł";
        itemNamesTMP.text = string.Join("\n", orderedItems.ConvertAll(p => p.name));
        deliveryTMP.text = deliveryInfo;
    }
}
