using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    private Vector3 originalScale;
    public static Inventory selectedSlot; 
    public bool isHammerUsed = false;
    public bool isMoneyUsed = false;
    public bool isHandUsed = false;
    public bool isKeyUsed = false;
    public bool isMedicineUsed = false;
    public Image SelectedItem;
   

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Awake()
    {
        selectedSlot = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var image = GetComponent<UnityEngine.UI.Image>();

        if (selectedSlot == this)
        {
            DeselectSlot();
            selectedSlot = null;


            if (SelectedItem != null)
                SelectedItem.sprite = null;

            switch (image.sprite.name)
            {
                case "Hammer": isHammerUsed = false; break;
                case "Money": isMoneyUsed = false; break;
                case "PlayerHand": isHandUsed = false; break;
                case "Key": isKeyUsed = false; break;
                case "Medicine": isMedicineUsed = false; break;
            }
            return;
        }

        if (selectedSlot != null)
        {
            selectedSlot.DeselectSlot();
            selectedSlot.isHammerUsed = false;
        }

        switch (image.sprite.name)
        {
            case "Hammer": isHammerUsed = true; break;
            case "Money": isMoneyUsed = true; break;
            case "PlayerHand": isHandUsed = true; break;
            case "Key": isKeyUsed = true; break;
            case "Medicine": isMedicineUsed = true; break;
        }
        SelectSlot();


        if (SelectedItem != null)
            SelectedItem.sprite = image.sprite;
            
            
    }
    



    

    private void SelectSlot()
    {

        selectedSlot = this;
        transform.localScale = originalScale * 1.2f;
    }

    private void DeselectSlot()
    {
        
        transform.localScale = originalScale; 
    }
}
