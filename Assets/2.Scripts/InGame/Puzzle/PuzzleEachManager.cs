using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleEachManager : MonoBehaviour
{
    [SerializeField]
    List<string> information = new List<string>();
    [SerializeField]
    protected int puzzleTotalFin = 0;
    [SerializeField]
    protected int puzzleTotal = 0;
    [SerializeField]
    private TMP_Text informationText;
    [SerializeField]
    private GameObject wrongImage;
    [SerializeField]
    private GameObject rightImage;


    private void Start()
    {
        puzzleTotal = gameObject.transform.GetChild(0).GetChild(0).childCount;
    }

    public virtual void answerWrong(int wrongNum)
    {
        //잘못된 답 넣었을 때 출력할 내용 override
    }

    public bool answerCompare(int emptyBlockIndex, int selectBlockIndex)
    {
        if (emptyBlockIndex == selectBlockIndex)
        {
            StartCoroutine(turnOnRightImage());
            puzzleTotalFin += 1;
            return true;
        }
        answerWrong(selectBlockIndex);
        StartCoroutine(turnOnWrongImage());
        return false;
    }

    public void setInfoText(int infoNum)
    {
        informationText.text = information[infoNum];
    }

    IEnumerator turnOnWrongImage()
    {
        wrongImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        wrongImage.SetActive(false);
    }

    IEnumerator turnOnRightImage()
    {
        rightImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        rightImage.SetActive(false);
    }
}
