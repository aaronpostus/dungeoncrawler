using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] SlidePuzzle puzzle;
    [SerializeField] int puzzlePieceNum;
    Material m;
    private void Start()
    {
        this.m = GetComponent<Renderer>().material;
    }
    private void OnMouseExit()
    {
        m.color = Color.white;
    }
    void OnMouseOver()
    {
        if (!puzzle.ShouldShowColors())
        {
            return;
        }
        if (puzzle.CanMove(puzzlePieceNum))
        {
            m.color = Color.green;
        }
        else if (!puzzle.IsMoving())
        {
            m.color = Color.red;
        }
        else
        {
            m.color = Color.white;
        }
        if (Input.GetMouseButtonDown(0))
        {
            puzzle.ClickAttempt(puzzlePieceNum);
        }
    }
    public int GetPuzzlePieceNum()
    {
        return puzzlePieceNum;
    }
}
