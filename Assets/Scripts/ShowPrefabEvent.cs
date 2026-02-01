using UnityEngine;

public class ShowPrefabEvent : MonoBehaviour
{
    public GameObject HandPrefab;  
 
    public void ShowMoney()
    {
        
        HandPrefab.SetActive(true);

    }
}
