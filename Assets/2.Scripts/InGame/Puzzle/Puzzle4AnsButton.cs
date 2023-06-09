using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4AnsButton : MonoBehaviour
{
    [SerializeField]
    private Puzzle4Manager puzzle4Manager;
    [SerializeField]
    private int attack;
    [SerializeField]
    private bool Right;

    private void Start()
    {
        puzzle4Manager = PuzzleManager.Instance.eachPuzzle[4].GetComponent<Puzzle4Manager>();
        changeAttack();
    }

    private void changeAttack()
    {
        int p = (puzzle4Manager.curTurn) * 2;
        if (Right)
            p++;
        attack = gameObject.transform.parent.transform.GetChild(p).GetComponent<Puzzle4Monster>().attack;
    }

    public void buttonPressed()
    {
        if (puzzle4Manager.Judge(attack))
            changeAttack();
    }
}
