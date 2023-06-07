using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle2Manager : PuzzleEachManager
{
    private void Start()
    {
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
    }
    public void finishedButtonPressed()
    {
        if (puzzleTotalFin >= puzzleTotal)
        {
            PuzzleManager.Instance.PuzzleEnd();
        }
        else
        {
            setInfoText(0);
        }
    }


    public override void answerWrong(int wrongNum)
    {
        base.answerWrong(wrongNum);
        setInfoText(wrongNum + 1);
    }


}
