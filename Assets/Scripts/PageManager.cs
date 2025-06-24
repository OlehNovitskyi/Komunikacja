using UnityEngine;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allPages;

    private GameObject currentPage;

    public void ShowPage(GameObject pageToShow)
    {
        if (currentPage != null)
            currentPage.SetActive(false);

        currentPage = pageToShow;
        currentPage.SetActive(true);

        foreach(GameObject Page in allPages)
        {
            if(Page != currentPage)
            {
                Page.SetActive(false);
            }
        }
    }
}
