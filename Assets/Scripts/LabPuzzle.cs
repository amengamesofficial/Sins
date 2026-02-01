using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class LabPuzzle : MonoBehaviour
{
    
    public Image[] slots;
    public Item[] items;
    public Image mainDrug;
    public SpriteRenderer drugObject;
    public GameObject labPuzzle;
    public GameObject labPuzzleIcon;
    public GameObject speak1;
    public GameObject speak2;
    public InteractableObject drugIcon;
    public GameObject playerUI;
    public GameObject returnButton;
    int counter = 0;
    bool canChoose = true;
    int result = 0;
    

    public void TobeKardan()
    {
        if(!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if(slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[0].image.sprite;
                slot.GetComponent<Image>().color = items[0].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void ImanAvardan()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[1].image.sprite;
                slot.GetComponent<Image>().color = items[1].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void KasbRooziHalal()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[2].image.sprite;
                slot.GetComponent<Image>().color = items[2].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void PardakhtHaghALnans()
    {
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[3].image.sprite;
                slot.GetComponent<Image>().color = items[3].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void DorooghGoftan()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[4].image.sprite;
                slot.GetComponent<Image>().color = items[4].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void DoaKardan()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null )
            {
                slot.GetComponent<Image>().sprite = items[5].image.sprite;
                slot.GetComponent<Image>().color = items[5].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void NikiBeMardom()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[6].image.sprite;
                slot.GetComponent<Image>().color = items[6].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void TohmatZadan()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[7].image.sprite;
                slot.GetComponent<Image>().color = items[7].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void TalabBakhsheshKardan()
    {
        result--;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[8].image.sprite;
                slot.GetComponent<Image>().color = items[8].image.color;
                break;
            }
            if (counter == 3)
            {
                Result();
            }
        }
    }

    public void Dozdikardan()
    {
        result++;
        if (!canChoose) return;
        counter++;
        foreach (var slot in slots)
        {
            
            if (slot.sprite == null)
            {
                slot.GetComponent<Image>().sprite = items[9].image.sprite;
                slot.GetComponent<Image>().color = items[9].image.color;
                break;
            }
             if( counter == 3)
            {
                Result();
            }
        }
    }

    void Result()
    {
        canChoose = false;
        StartCoroutine(AfterResult()); 
    }

    IEnumerator AfterResult()
    {
        yield return new WaitForSeconds(1);

        if(result == -1)
        {
            returnButton.SetActive(false);
            mainDrug.enabled = true;
            drugObject.color = Color.white;
            speak2.SetActive(true);
            yield return new WaitForSeconds(6);
            speak2.SetActive(false);
            playerUI.SetActive(true);
            drugIcon.enabled = true;
            returnButton.SetActive(true);
            Destroy(labPuzzleIcon);
            Destroy(labPuzzle);
        }
        else
        {
            returnButton.SetActive(false);
            speak1.SetActive(true);
            yield return new WaitForSeconds(4);
            foreach (var slot in slots)
            {

                slot.GetComponent<Image>().sprite = null;
                slot.GetComponent<Image>().color = Color.white;
                counter = 0;
                result = 0;
                canChoose = true;
            }
            speak1.SetActive(false);
            returnButton.SetActive(true);
        }

        

            
    }

    public void Exit()
    {
        labPuzzle.SetActive(false);
        playerUI.SetActive(true);
        labPuzzleIcon.SetActive(true);

    }

    [System.Serializable]
    public class Item
    {
        public Image image;
        public string imageName;
    }
}


