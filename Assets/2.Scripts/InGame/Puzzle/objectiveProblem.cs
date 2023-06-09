using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveProblem : MonoBehaviour
{
    [SerializeField]
    private int index;
    [SerializeField]
    private Puzzle3Manager puzzle3Manager;

    public void buttonPressed()
    {
        puzzle3Manager.answerCompareButton(index);
    }
}
