using UnityEngine;

public class ManualSmartOffsetActivator : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform Rightlimit;

    [Header("Placement Settings")]
    [Tooltip("فاصله از سمت راست پلیر")]
    public float offsetX = 5f; 
    [Tooltip("آفست ارتفاع")]
    public float offsetY = 0f;

    private Vector3 originalScale;

    void Awake()
    {
        
        originalScale = transform.localScale;
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void OnEnable()
{
    if (player == null || Rightlimit == null) return;

    float finalOffsetX = offsetX;

    
    if (player.position.x + offsetX > Rightlimit.position.x)
    {
        finalOffsetX = -offsetX;
    }

    Vector3 spawnPos = player.position;
    spawnPos.x += finalOffsetX;
    spawnPos.y += offsetY;

    transform.position = spawnPos;

    
    Vector3 scale = originalScale;
    scale.x = (finalOffsetX > 0)
        ? Mathf.Abs(originalScale.x)
        : -Mathf.Abs(originalScale.x);

    transform.localScale = scale;
}


    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Vector3 previewPos = player.position + new Vector3(offsetX, offsetY, 0);
            Gizmos.DrawWireCube(previewPos, new Vector3(1, 2, 1));
            Gizmos.DrawLine(player.position, previewPos);
        }
    }
}