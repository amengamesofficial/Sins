using UnityEngine;
using TMPro;
using RTLTMPro;
using System.Xml.Serialization;
using System.Collections;
using Unity.VisualScripting;

public class SafeLock : MonoBehaviour
{
    public static SafeLock instance;
    [Header("UI References")]
    public RTLTextMeshPro displayText;

    [Header("Settings")]
    public GameObject AngleUi;
    public GameObject SafeBoxPad;
    public GameObject ClearButton;
    public GameObject CheckingButton;
    public GameObject CloseButton;
    public GameObject OpenPadButton;
    public GameObject puzzleGuide;
    public CircleCollider2D circleCollider;

    



    public string correctCode = "123";

    private string currentInput = "";
    
    void Awake()
    {
        instance = this;
    }

    public void AddNumber(int number)
    {
        if (currentInput.Length < 6)
        {
            currentInput += number.ToString();
            displayText.text = currentInput;
        }
    }

    public void ClearInput()
    {
        currentInput = "";
        displayText.text = "";
    }

    public void CheckCode()
    {
        if (currentInput == correctCode)
        {
           displayText.text = "صحیح";
           puzzleGuide.SetActive(false);
           circleCollider.enabled = false; 
           StartCoroutine(AfterCorrectCode());

        }
        else
        {
            displayText.text = "خطا";
            
            Invoke(nameof(ClearInput), 1.5f);
        }
    }

    IEnumerator AfterCorrectCode()
    {
        ClearButton.SetActive(false);
        CheckingButton.SetActive(false);
        CloseButton.SetActive(false);
        

        yield return new WaitForSeconds(1.5f);
        ClosePad();
        AngleUi.SetActive(true);
        Destroy(OpenPadButton);
        SafeBoxOpen.instance.OpenSafeBox();
        
        
    }
    
    public void ClosePad()
    {
        SafeBoxPad.SetActive(false);
        AngleUi.SetActive(true);
    }
}
