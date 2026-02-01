using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class PuzzlePiece : MonoBehaviour
{
    public int pieceID;
    public int id;              
    [HideInInspector] public PuzzleManager manager;
    private void Update()
    {
        id = transform.GetSiblingIndex();
    }

    void Start()
    {
        manager = FindObjectOfType<PuzzleManager>();
        GetComponent<Button>().onClick.AddListener(() => manager.TryMovePiece(this));
    }

    
    public IEnumerator MoveTo(Vector3 target, float speed)
    {
        Vector3 start = transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        transform.position = target;
    }
}
