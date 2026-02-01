using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class PuzzleManager : MonoBehaviour
{
    public GameObject Puzzle;
    public GameObject PuzzleImage;
    public GameObject Key;
    public GameObject playerUI;
    public Image LastKeyPuzzle;
    public GameObject ClosePuzzle;

    [Header("Reference to empty slot")]
    public RectTransform emptySpace;

    [Header("All puzzle pieces")]
    public PuzzlePiece[] pieces;

    [Header("Settings")]
    public float moveSpeed = 5f;

    private bool isSolved = false;

    public void TryMovePiece(PuzzlePiece piece)
    {
        if (isSolved) return;
        if (IsNeighbor(piece.transform as RectTransform, emptySpace))
        {
            Vector3 oldPiecePos = piece.transform.position;
            StartCoroutine(MovePieceAndSwap(piece, oldPiecePos));
        }
    }

    bool IsNeighbor(RectTransform a, RectTransform b)
    {
        float dist = Vector3.Distance(a.localPosition, b.localPosition);
        return dist < (a.rect.width + 10f);
    }




    IEnumerator MovePieceAndSwap(PuzzlePiece piece, Vector3 emptyOldPos)
    {
        yield return StartCoroutine(piece.MoveTo(emptySpace.position, moveSpeed));

        int emptyIndex = emptySpace.GetSiblingIndex();
        int pieceIndex = piece.transform.GetSiblingIndex();

        piece.transform.SetSiblingIndex(emptyIndex);
        emptySpace.SetSiblingIndex(pieceIndex);

        emptySpace.position = emptyOldPos;

        CheckIfSolved();
    }

    void CheckIfSolved()
    {
        foreach (var p in pieces)
        {
            if (p.transform.GetSiblingIndex() != p.pieceID)
                return;
        }

        
       isSolved = true;
       ClosePuzzle.SetActive(false);

        StartCoroutine(AfterSolvedPuzzle());
    }


    IEnumerator AfterSolvedPuzzle()
    {
        yield return new WaitForSeconds(1f);
        LastKeyPuzzle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Key.SetActive(true);
        playerUI.SetActive(true);
        PuzzleImage.SetActive(false);
        Puzzle.SetActive(false);
        
        
        
    }
}
