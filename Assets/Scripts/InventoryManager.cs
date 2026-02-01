using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Image[] items; 

    private void Awake()
    {
        
        if (Instance == null) Instance = this;
        gameObject.SetActive(false);
    }


    public void AddItem(Sprite itemIcon)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].sprite == null) 
            {
                items[i].sprite = itemIcon; 
                items[i].enabled = true;
                items[i].color = Color.white; 
                break;
            }
        }
    }
}
