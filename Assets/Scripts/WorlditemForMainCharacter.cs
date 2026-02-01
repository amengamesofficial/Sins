
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class WorlditemForMainCharacter : MonoBehaviour
{
    
    
    public GameObject HandDoor;
    public GameObject FootDoor;
    public GameObject BellyDoor;
    public GameObject AfterUseEyeDialog;
    public GameObject AfterUseHandDialog;
    public GameObject AfterUseFootDialog;
    public GameObject AfterUseBellyDialog;
    public GameObject sceneFinilizer;
    public GameObject Player;
    public GameObject SickPlayer;
     public GameObject PlayerUI;
    public GameObject EndPart1;
   





    private void OnMouseDown()
    {

        if (gameObject.tag == "Eye" && Inventory.selectedSlot.isHammerUsed) StartCoroutine(AfterUseEye());
        else if (gameObject.tag == "Hand" && Inventory.selectedSlot.isHandUsed) StartCoroutine(AfterUseHand());
        else if (gameObject.tag == "Foot" && Inventory.selectedSlot.isKeyUsed) StartCoroutine(AfterUseFoot());
        else if (gameObject.tag == "Belly" && Inventory.selectedSlot.isMedicineUsed) StartCoroutine(AfterUseBelly());


    }



    IEnumerator AfterUseEye()
    {
        AngleMovment.instance.Use();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.eyeanimation = true;
        InventoryManager.Instance.items[0].sprite = null;
        Inventory.selectedSlot.SelectedItem.sprite = null;
        HandDoor.SetActive(true);
        AfterUseEyeDialog.SetActive(true);
        Destroy(gameObject);
 

    }
    IEnumerator AfterUseHand()
    {
        GameManager.instance.eyeanimation = false;
        AngleMovment.instance.Use();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.handanimation = true;
        InventoryManager.Instance.items[0].sprite = null;
        Inventory.selectedSlot.SelectedItem.sprite = null;
        FootDoor.SetActive(true);
        AfterUseHandDialog.SetActive(true);
        Destroy(gameObject);
        
    }

    IEnumerator AfterUseFoot()
    {
        GameManager.instance.handanimation = false;
        AngleMovment.instance.Use();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.footanimation = true;
        InventoryManager.Instance.items[0].sprite = null;
        Inventory.selectedSlot.SelectedItem.sprite = null;
        BellyDoor.SetActive(true);
        AfterUseFootDialog.SetActive(true);
        Destroy(gameObject);
        
    }
    IEnumerator AfterUseBelly()
    {
        
        GameManager.instance.footanimation = false;
        AngleMovment.instance.Use();
        gameObject.GetComponent<SpriteRenderer>().sprite = null; 
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.bellyanimation = true;
        InventoryManager.Instance.items[0].sprite = null;
        Inventory.selectedSlot.SelectedItem.sprite = null;
        yield return new WaitForSeconds(1);
        sceneFinilizer.SetActive(true);
        yield return new WaitForSeconds(3f);
        SickPlayer.SetActive(false);
        Player.SetActive(true);
        sceneFinilizer.SetActive(false);
        AfterUseBellyDialog.SetActive(true);
        EndPart1.SetActive(true);
        Destroy(gameObject);
        

    }
}
    

    

 


    


