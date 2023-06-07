using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle0Manager : PuzzleEachManager
{
    [SerializeField]
    private TMP_Text myagePlaceholder;
    [SerializeField]
    private TMP_Text mynamePlaceholder;
    [SerializeField]
    private TMP_Text myageShow;
    [SerializeField]
    private TMP_Text mynameShow;
    [SerializeField]
    private GameObject finalCheck;
    [SerializeField]
    private TMP_Text finalCheckText;

    private void Start()
    {
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
    }
    public void finishedButtonPressed()
    {
        if(puzzleTotalFin >= puzzleTotal)
        {
            //����
            if(myagePlaceholder.text.Length == 1 || mynamePlaceholder.text.Length == 1)
            {
                setInfoText(4);
                return;
            }

            GameManager.Instance.playerName = mynamePlaceholder.text;
            GameManager.Instance.playerAge = myagePlaceholder.text;

            finalCheck.SetActive(true);
            finalCheckText.text = "����� �̸��� : " + mynamePlaceholder.text + "\n����� ���̴� : " + myagePlaceholder.text + "\n �̴� ���Ŀ� ���� �� �� �����ϴ�.";

        }
        else
        {
            //�������ʾҵ��� info �����ֱ�
            setInfoText(0);
        }

    }

    public void puzzleReallyFinishButton()
    {
        PuzzleManager.Instance.PuzzleEnd();
    }

    public void puzzleReallyNotFinishButton()
    {
        finalCheck.SetActive(false);
    }

    public void myageEdit()
    {
        myageShow.text = "MyAge = " + myagePlaceholder.text;
    }
    public void mynameEdit()
    {
        mynameShow.text = "MyName = " + mynamePlaceholder.text;
    }

    public override void answerWrong(int wrongNum)
    {
        base.answerWrong(wrongNum);
        if(wrongNum == 0)
        {
            setInfoText(1);
        }
        else if (wrongNum == 1)
        {
            setInfoText(2);
        }
        else if (wrongNum == 2)
        {
            setInfoText(3);

        }
    }


}
