using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalSummary : MonoBehaviour
{
    [Header("Cart Manager")]
    public CartManager cartManager;

    [Header("History former")]
    public HistoryFormer historyFormer;

    [Header("UI Elements")]
    public TextMeshProUGUI totalPriceText;
    public TextMeshProUGUI userInfoText;

    [Header("User Input Fields")]
    public TMP_InputField nameInput;
    public TMP_InputField surnameInput;
    public TMP_InputField cityInput;
    public TMP_InputField streetInput;
    public TMP_Dropdown deliveryDropdown;

    private List<ProductData> cartItems;
    private float total;

    public void GenerateSummary()
    {
        total = 0f;

        cartItems = cartManager.GetCartItems();
        foreach (var item in cartItems)
        {
            total += item.price;
        }

        if (totalPriceText != null)
            totalPriceText.text = $"Total: {total:0.00} zł";

        string deliveryMethod = deliveryDropdown != null && deliveryDropdown.options.Count > 0
            ? deliveryDropdown.options[deliveryDropdown.value].text
            : "No delivery option selected";

        if (userInfoText != null)
        {
            userInfoText.text =
                $"Name: {nameInput.text}\n" +
                $"Surname: {surnameInput.text}\n" +
                $"City: {cityInput.text}\n" +
                $"Street: {streetInput.text}\n" +
                $"Delivery: {deliveryMethod}";
        }
    }

    public void FinalizeOrder()
    {
        historyFormer.AddOrderToHistory(cartItems, total, userInfoText.text);
        cartManager.ClearCart();
    }
}
