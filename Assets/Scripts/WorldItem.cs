using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class WorldItem : MonoBehaviour
{
    GameObject itemObject;
    Sprite itemIcon;
    public GameObject sceneFinilizer;
    public bool canBePickedUp = true;
    public bool canBeUsed = false;
    public GameObject EndMission;
    public GameObject PlayerUi;

    [Header("For EyeScene")]

    public Sprite brokenlaptop;

    [Header("For KindScene")]
    public GameObject AfterGiveMoney;
    public GameObject AftergiveHand;

    [Header("For PuzzleScene")]
    public GameObject AfterGetKeyDialog;

    [Header("For LaboratoryScene")]
    public GameObject AfterGetMedicineDialog;
    





    private void Start()
    {
        
        itemIcon = transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;
        itemObject = transform.parent.gameObject;

    }


    private void OnMouseDown()
    {
        if (canBePickedUp)
        {


            switch (itemObject.tag)
            {
                case "Hammer":
                    AngleMovment.instance.PickUp();
                    StartCoroutine(DelayedAddAndDestroy());
                    break;

                case "Money":
                    AngleMovment.instance.PickUp();
                    StartCoroutine(GetMoney());
                    break;


                case "Hand":
                    AngleMovment.instance.PickUp();
                    StartCoroutine(AfterGetHand());
                    break;

                case "Key":
                    AngleMovment.instance.PickUp();
                    StartCoroutine(AfterGetKey());
                    break;

                case "Medicine":
                    AngleMovment.instance.PickUp();
                    StartCoroutine(AfterGetMedicine());
                    break;


            }
        }

        if (canBeUsed)
        {
            switch (itemObject.tag)
            {
                case "laptop":

                    if (Inventory.selectedSlot.isHammerUsed)
                    {
                        AngleMovment.instance.Use();
                        StartCoroutine(BrokenLaptopUse());

                    }
                    
                    break;

                case "Poor":
                    if (Inventory.selectedSlot.isMoneyUsed)
                    {

                        AngleMovment.instance.Use();
                        StartCoroutine(PoorGiveMoney());
                    }
                    break;

                

            }
        }

    }

    IEnumerator DelayedAddAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.AddItem(itemIcon);
        Destroy(itemObject);

    }

    IEnumerator BrokenLaptopUse()
    {
        yield return new WaitForSeconds(0.5f);
        itemObject.GetComponent<SpriteRenderer>().sprite = brokenlaptop;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<Collider2D>().enabled = false;
        ScoreManager.Instance.Score += 1;
        yield return new WaitForSeconds(0.5f);
        if (ScoreManager.Instance.Score == 2) EndMission.SetActive(true);


    }

    IEnumerator GetMoney()
    {
        yield return new WaitForSeconds(0.5f);
        ScoreManager.Instance.Score += 1;

        if (ScoreManager.Instance.Score == 3)
        {
            InventoryManager.Instance.AddItem(itemIcon);
            Poor.instance.Poor2.SetActive(true);
            Poor.instance.isGetMoney = true;
            Poor.instance.GetComponent<Collider2D>().enabled = true;
        }
        Destroy(itemObject);



    }

    IEnumerator PoorGiveMoney()
    {
        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.items[0].sprite = null;
        Inventory.selectedSlot.SelectedItem.sprite = null;
        Poor.instance.AcceptsAnimation();

        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<Collider2D>().enabled = false;
        PlayerUi.SetActive(false);

        yield return new WaitForSeconds(2f);
        Poor.instance.AfterGetMoney();
        AfterGiveMoney.SetActive(true);

        while (AfterGiveMoney.transform.GetChild(0).gameObject.activeSelf)
            yield return null;
      

        Poor.instance.GiveHand();


    }

    IEnumerator AfterGetHand()
    {
        Poor.instance.AcceptsAnimation();

        yield return new WaitForSeconds(0.5f);
          if(PlayerUi.activeSelf == false)
            PlayerUi.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Poor.instance.AfterGetMoney();
        AftergiveHand.SetActive(true);
        EndMission.SetActive(true);
        Destroy(itemObject);


    }

    IEnumerator AfterGetKey()
    {

        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.AddItem(itemIcon);
        yield return new WaitForSeconds(0.3f);
        AfterGetKeyDialog.SetActive(true);
        EndMission.SetActive(true);
        Destroy(itemObject);

    }
    
    IEnumerator AfterGetMedicine()
    {
        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.AddItem(itemIcon);
        yield return new WaitForSeconds(0.3f);
        AfterGetMedicineDialog.SetActive(true);
        EndMission.SetActive(true);
        Destroy(itemObject);
        
    }
}

    

