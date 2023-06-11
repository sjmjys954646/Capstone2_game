using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle4Manager : PuzzleEachManager
{
    [SerializeField]
    private TMP_Text damageShow;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    public int curTurn = 0;
    [SerializeField]
    public List<GameObject> monsters = new List<GameObject>();
    [SerializeField]
    private TMP_Text leftQuestion;
    [SerializeField]
    private TMP_Text rightQuestion;



    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        setInfoText(0); 
        curTurn = 0;
        damage = 5;
        puzzleTotalFin = 0;
        setDamageText();
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

    private void setDamageText()
    {
        char p = '<';
        string pp = "플레이어 공격력 : " + damage;
        string ppp = "if  ( " + monsters[curTurn * 2].GetComponent<Puzzle4Monster>().attack+ "   " + p +"   " 
            + damage + " ) :\n   continue";

        string pppp = "if  ( " + monsters[curTurn * 2 + 1].GetComponent<Puzzle4Monster>().attack + "   " + p + "   "
            + damage + " ) :\n   continue";

        damageShow.text = pp;
        leftQuestion.text = ppp;
        rightQuestion.text = pppp;
    }

    public void addDamage(int dmg)
    {
        damage += dmg;
    }

    public int curDamage()
    {
        return damage;
    }

    public bool Judge(int attack)
    {
        //정답
        if (damage >= attack)
        {
            addDamage(attack);
            puzzleTotalFin++;
            monsters[curTurn * 2].SetActive(false);
            monsters[curTurn * 2 + 1].SetActive(false);


            if (puzzleTotalFin == puzzleTotal)
            {
                PuzzleManager.Instance.PuzzleEnd();
                return true;
            }
            StartCoroutine(turnOnRightImage());
            curTurn++;
            setDamageText();
            monsters[curTurn * 2].SetActive(true);
            monsters[curTurn * 2 + 1].SetActive(true);
            setInfoText(2);

            return true;

        }
        else //오답
        {
            setInfoText(1);
            StartCoroutine(turnOnWrongImage());
            return false;
        }
    }

}
