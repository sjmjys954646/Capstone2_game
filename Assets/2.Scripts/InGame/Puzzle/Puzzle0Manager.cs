using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle0Manager : MonoBehaviour
{
    public bool answerCompare(int a, int b)
    {
        Debug.Log(a);
        Debug.Log(b);
        if (a == b)
            return true;

        return false;
    }
}
