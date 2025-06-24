using UnityEngine;

public class TransferScript : MonoBehaviour
{
    public PageManager pageManager;
    public GameObject targetPage;

    public void Transfer()
    {
        pageManager.ShowPage(targetPage);
    }
}
