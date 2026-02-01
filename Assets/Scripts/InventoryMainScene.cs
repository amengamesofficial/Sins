using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMainScene : MonoBehaviour
{
    public Sprite HammerIcon;
    public Sprite HandIcon;
    public Sprite KeyIcon;
    public Sprite MedicineIcon;

    void Start()
    {
        if (GameManager.instance.eyedooropen && !GameManager.instance.handdooropen) InventoryManager.Instance.AddItem(HammerIcon);
        else if (GameManager.instance.handdooropen && !GameManager.instance.footdooropen) InventoryManager.Instance.AddItem(HandIcon);
        else if (GameManager.instance.footdooropen && !GameManager.instance.bellydooropen) InventoryManager.Instance.AddItem(KeyIcon);
        else if (GameManager.instance.bellydooropen) InventoryManager.Instance.AddItem(MedicineIcon);
    }
}
