using UnityEngine;
using UnityEngine.UI;

public class SafeBoxPassword : MonoBehaviour
{
    [Header("Button Settings")]
    public int buttonID;              
    [Header("References")]
    public SafeLock safeLock;         

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        if (safeLock != null)
        {
            
            safeLock.AddNumber(buttonID);
        }
       
    }
}
