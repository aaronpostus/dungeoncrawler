using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

public class SlidePuzzle
{
    [SerializeField] List<int> solutionRow1, solutionRow2, solutionRow3;
    [SerializeField] List<PuzzlePiece> pieces;
    [SerializeField] GameObject waitScreen;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip startGame, clickPiece, gameWin;
    [SerializeField] List<Renderer> shitToFadeOut;
    [SerializeField] Renderer picToFadeIn;
    const int EMPTY_PIECE = -1;
    const float HORIZONTAL_MOVE_DISTANCE = 1 / 3f;
    const float VERTICAL_MOVE_DISTANCE = 1 / 3f;
    List<int> row1State = new List<int> { 2, 8, 4 };
    List<int> row2State = new List<int> { 7, 1, EMPTY_PIECE };
    List<int> row3State = new List<int> { 3, 6, 5 };

    enum PuzzleState { UNSOLVED, MOVING_PIECE, SOLVED_FADING_OUT, SOLVED }
    PuzzleState state = PuzzleState.UNSOLVED;

    PuzzlePiece currPiece;
    Vector3 destinationPos = Vector3.zero;
    float opacity = 1f, gazeAtPic = 2f;
    public bool ShouldShowColors()
    {
        return true;
    }
    public bool IsMoving()
    {
        return state == PuzzleState.MOVING_PIECE;
    }
    public bool CanMove(int pieceNum)
    {
        if (state != PuzzleState.UNSOLVED)
        {
            return false;
        }
        return AdjacentHorizontal(pieceNum) || AdjacentVertical(pieceNum);
    }

    // precondition: pieceNum \in currState
    public void ClickAttempt(int pieceNum)
    {
        if (state != PuzzleState.UNSOLVED)
        {
            // solved or moving a piece already
            return;
        }
        if (AdjacentHorizontal(pieceNum))
        {
            state = PuzzleState.MOVING_PIECE;
            int pieceIndex, emptyIndex;
            if (row1State.Contains(pieceNum))
            {
                pieceIndex = row1State.IndexOf(pieceNum);
                emptyIndex = row1State.IndexOf(EMPTY_PIECE);
            }
            else if (row2State.Contains(pieceNum))
            {
                pieceIndex = row2State.IndexOf(pieceNum);
                emptyIndex = row2State.IndexOf(EMPTY_PIECE);
            }
            else
            {
                pieceIndex = row3State.IndexOf(pieceNum);
                emptyIndex = row3State.IndexOf(EMPTY_PIECE);
            }
            currPiece = GetPieceByNum(pieceNum);
            destinationPos = currPiece.transform.localPosition;
            if (pieceIndex > emptyIndex)
            {
                // MOVE PIECE LEFT
                destinationPos.z += HORIZONTAL_MOVE_DISTANCE;
                src.PlayOneShot(clickPiece, 0.7f);
            }
            else
            {
                // MOVE PIECE RIGHT
                destinationPos.z -= HORIZONTAL_MOVE_DISTANCE;
                src.PlayOneShot(clickPiece, 0.7f);
            }
        }
        if (AdjacentVertical(pieceNum))
        {
            state = PuzzleState.MOVING_PIECE;
            currPiece = GetPieceByNum(pieceNum);
            destinationPos = currPiece.transform.localPosition;
            if (row1State.Contains(pieceNum))
            {
                // MOVE PIECE DOWN
                destinationPos.x -= VERTICAL_MOVE_DISTANCE;
                src.PlayOneShot(clickPiece, 0.7f);
            }
            else if (row3State.Contains(pieceNum))
            {
                // MOVE PIECE UP
                destinationPos.x += VERTICAL_MOVE_DISTANCE;
                src.PlayOneShot(clickPiece, 0.7f);
            }
            else
            {
                if (row1State.Contains(EMPTY_PIECE))
                {
                    // MOVE PIECE UP
                    destinationPos.x += VERTICAL_MOVE_DISTANCE;
                    src.PlayOneShot(clickPiece, 0.7f);
                }
                else
                {
                    // MOVE PIECE DOWN
                    destinationPos.x -= VERTICAL_MOVE_DISTANCE;
                    src.PlayOneShot(clickPiece, 0.7f);
                }
            }
        }
        return;
    }
    private PuzzlePiece GetPieceByNum(int num)
    {
        foreach (PuzzlePiece piece in pieces)
        {
            if (piece.GetPuzzlePieceNum() == num)
            {
                return piece;
            }
        }
        return null;
    }
    private bool AdjacentHorizontal(int x)
    {
        // check if diff rows
        int emptyPieceRow;
        int pieceNumRow;
        if (row1State.Contains(EMPTY_PIECE)) { emptyPieceRow = 1; }
        else if (row2State.Contains(EMPTY_PIECE)) { emptyPieceRow = 2; }
        else { emptyPieceRow = 3; }
        if (row1State.Contains(x)) { pieceNumRow = 1; }
        else if (row2State.Contains(x)) { pieceNumRow = 2; }
        else { pieceNumRow = 3; }
        if (emptyPieceRow != pieceNumRow) { return false; }

        if (emptyPieceRow == 1)
        {
            return Mathf.Abs(row1State.IndexOf(x) - row1State.IndexOf(EMPTY_PIECE)) == 1;
        }
        else if (emptyPieceRow == 2)
        {
            return Mathf.Abs(row2State.IndexOf(x) - row2State.IndexOf(EMPTY_PIECE)) == 1;
        }
        else
        {
            return Mathf.Abs(row3State.IndexOf(x) - row3State.IndexOf(EMPTY_PIECE)) == 1;
        }
    }
    private bool AdjacentVertical(int x)
    {
        // same row so not vertical
        if (AdjacentHorizontal(x))
        {
            return false;
        }
        int pieceIndex, emptyPieceIndex;
        if (row1State.Contains(x))
        {
            if (!row2State.Contains(EMPTY_PIECE)) { return false; }
            pieceIndex = row1State.IndexOf(x);
            emptyPieceIndex = row2State.IndexOf(EMPTY_PIECE);
        }
        else if (row2State.Contains(x))
        {
            pieceIndex = row2State.IndexOf(x);
            if (row1State.Contains(EMPTY_PIECE))
            {
                emptyPieceIndex = row1State.IndexOf(EMPTY_PIECE);
            }
            else
            {
                emptyPieceIndex = row3State.IndexOf(EMPTY_PIECE);
            }
        }
        else
        {
            pieceIndex = row3State.IndexOf(x);
            if (!row2State.Contains(EMPTY_PIECE)) { return false; }
            emptyPieceIndex = row2State.IndexOf(EMPTY_PIECE);
        }
        return pieceIndex == emptyPieceIndex;
    }

    private void CheckSolved()
    {
        for (int i = 0; i < solutionRow1.Count; i++)
        {
            if (solutionRow1[i] != row1State[i])
            {
                return;
            }
            if (solutionRow2[i] != row2State[i])
            {
                return;
            }
            if (solutionRow3[i] != row3State[i])
            {
                return;
            }
        }
        state = PuzzleState.SOLVED_FADING_OUT;

        src.PlayOneShot(gameWin);
    }
    private void DisplayCongratsMessage()
    {
        this.text.text = "CONGRATULATIONS ON SOLVING THIS PUZZLE. YOU HAVE BEEN AWARDED A KEY FOR YOUR EFFORTS.";
        this.waitScreen.SetActive(true);
    }
    // Swaps empty piece and moved piece
    private void ChangeStateOfPieces(int x)
    {
        if (AdjacentHorizontal(x))
        {
            if (row1State.Contains(x))
            {
                int emptyIndex = row1State.IndexOf(EMPTY_PIECE);
                row1State[row1State.IndexOf(x)] = EMPTY_PIECE;
                row1State[emptyIndex] = x;
            }
            else if (row2State.Contains(x))
            {
                int emptyIndex = row2State.IndexOf(EMPTY_PIECE);
                row2State[row2State.IndexOf(x)] = EMPTY_PIECE;
                row2State[emptyIndex] = x;
            }
            else
            {
                int emptyIndex = row3State.IndexOf(EMPTY_PIECE);
                row3State[row3State.IndexOf(x)] = EMPTY_PIECE;
                row3State[emptyIndex] = x;
            }
        }
        else
        {
            int emptyIndex;
            if (row1State.Contains(x))
            {
                emptyIndex = row2State.IndexOf(EMPTY_PIECE);
                row1State[row1State.IndexOf(x)] = EMPTY_PIECE;
                row2State[emptyIndex] = x;
            }
            else if (row3State.Contains(x))
            {
                emptyIndex = row2State.IndexOf(EMPTY_PIECE);
                row3State[row3State.IndexOf(x)] = EMPTY_PIECE;
                row2State[emptyIndex] = x;
            }
            else
            {
                if (row1State.Contains(EMPTY_PIECE))
                {
                    emptyIndex = row1State.IndexOf(EMPTY_PIECE);
                    row2State[row2State.IndexOf(x)] = EMPTY_PIECE;
                    row1State[emptyIndex] = x;
                }
                else
                {
                    emptyIndex = row3State.IndexOf(EMPTY_PIECE);
                    row2State[row2State.IndexOf(x)] = EMPTY_PIECE;
                    row3State[emptyIndex] = x;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (state == PuzzleState.MOVING_PIECE)
        {
            if (currPiece.transform.localPosition == destinationPos)
            {
                state = PuzzleState.UNSOLVED;
                // PERFORM SWAP IN STATE LISTS
                ChangeStateOfPieces(currPiece.GetPuzzlePieceNum());
                return;
            }
            currPiece.transform.localPosition = Vector3.MoveTowards(currPiece.transform.localPosition, destinationPos, Time.deltaTime);
        }
        if (state == PuzzleState.SOLVED_FADING_OUT)
        {
            opacity -= Time.deltaTime;
            float tempOpacity = Mathf.Max(opacity, 0.05f);
            foreach (Renderer rend in shitToFadeOut)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, tempOpacity);
            }
            float newVal = Mathf.Min(((1 - Mathf.Max(0, opacity))), 1f);
            picToFadeIn.material.color = new Color(newVal, newVal, newVal);
            if (tempOpacity == 0.05f && newVal == 1f)
            {
                state = PuzzleState.SOLVED;
                DisplayCongratsMessage();
                foreach (Renderer rend in shitToFadeOut)
                {
                    rend.enabled = false;
                }
            }
            return;
        }
        CheckSolved();
    }
}
