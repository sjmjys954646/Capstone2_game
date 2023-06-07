using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle1Manager : PuzzleEachManager
{
    [SerializeField]
    private List<GameObject> blankObject = new List<GameObject>();
    [SerializeField]
    private List<GameObject> finishedObject = new List<GameObject>();
    private void Start()
    {
        for(int i = 0;i< 4 ;i++)
        {
            blankObject.Add(gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
            finishedObject.Add(gameObject.transform.GetChild(1).GetChild(i).gameObject);
        }
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
    }

    public void finishedButtonPressed()
    {
        if (puzzleTotalFin >= puzzleTotal)
        {
            int ans = 0;
            for (int i = 0; i < 4; i++)
            {
                if (finishedObject[i].GetComponent<PuzzleBlock>().finished)
                {
                    ans++;
                }
            }

            if(ans == 4)
            {
                PuzzleManager.Instance.PuzzleEnd();
            }
            else
            {
                answerWrong(1);
                for (int i = 0; i < 4; i++)
                {
                    finishedObject[i].transform.position = finishedObject[i].GetComponent<PuzzleBlock>().getInitialPos();
                    finishedObject[i].GetComponent<PuzzleBlock>().finished = false;
                }
                puzzleTotalFin = 0;
            }
        }
        else
        {
            //끝나지않았따고 info 보여주기
            setInfoText(0);
        }

    }


    public override void answerWrong(int wrongNum)
    {
        base.answerWrong(wrongNum);
        setInfoText(1);
    }

}
