using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle2Manager : PuzzleEachManager
{
    private void Start()
    {
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
        puzzleTotalFin = 0;
    }

    public void Restart()
    {
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
        puzzleTotalFin = 0;
        foreach(GameObject elem in gameObject.transform.GetChild(0).GetChild(0))
        {
            elem.transform.position = elem.GetComponent<PuzzleBlock>().getInitialPos();
        }
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
