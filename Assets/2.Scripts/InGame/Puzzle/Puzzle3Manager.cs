using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puzzle3Manager : PuzzleEachManager
{
    [SerializeField]
    private TMP_Text QuestionText;
    [SerializeField]
    private TMP_Text CurCorrectText;
    [SerializeField]
    private List<TMP_Text> answerText = new List<TMP_Text>();

    private int ans;
    private int curInvenNum;
    private Vector3 mvVec = new Vector3(0, 50, 0);
    private int ansIdx;

    private void Start()
    {
        StartCoroutine(startpuzzle());
    }


    public void answerCompareButton(int pressed)
    {
        if (pressed == ansIdx)
        {
            puzzleTotalFin++;
            correctTextChange();
            StartQuestion();
            setInfoText(2);
        }
        else
        {
            puzzleTotalFin = 0;
            wrongTextChange(); 
            StartQuestion();
            setInfoText(1);
        }
    }

    private void StartQuestion()
    {
        if (puzzleTotalFin == puzzleTotal)
        {
            InventoryManager.Instance.CloseInventory();
            InventoryManager.Instance.SetInventoryUIPos(-mvVec);
            PuzzleManager.Instance.PuzzleEnd();
            return;
        }
        selectAns();
        makeQuestion();
        makeAnswer();
    }

    private void selectAns()
    {
        ans = Random.Range(0, curInvenNum);
    }

    private void makeQuestion()
    {
        string tt = "inventory[" + ans / 5 + "][" + ans % 5 + "] 아이템의 이름은 ?";
        QuestionText.text = tt;
    }

    private void makeAnswer()
    {
        int[] visited = new int[curInvenNum];
        ansIdx = Random.Range(0, 4);
        visited[ans] = 1;

        for(int i = 0;i < 4 ;i++)
        {
            if(i == ansIdx)
            {
                answerText[i].text = InventoryManager.Instance.InventoryShow()[ans].showItemName();
            }
            else
            {
                int p;
                while(true)
                {
                    p = Random.Range(0, curInvenNum);
                    if (visited[p] != 1)
                        break;
                }
                string tt = InventoryManager.Instance.InventoryShow()[p].showItemName();
                answerText[i].text = tt;
                visited[p] = 1;
            }
        }

    }

    private void correctTextChange()
    {
        string tt = "(" + puzzleTotalFin + " / 3)";
        CurCorrectText.text = tt;
    }

    private void wrongTextChange()
    {
        string tt = "( 0 / 3 )";
        CurCorrectText.text = tt;
    }

    public override void answerWrong(int wrongNum)
    {
        base.answerWrong(wrongNum);
        setInfoText(wrongNum);
    }

    IEnumerator startpuzzle()
    {
        yield return new WaitForSeconds(3.1f);

        InventoryManager.Instance.OpenInventory();
        InventoryManager.Instance.SetInventoryUIPos(mvVec);
        curInvenNum = InventoryManager.Instance.InventoryContentNum();
        setInfoText(0);
        StartQuestion();
    }
}
