using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Puzzle4Monster : MonoBehaviour
{
    [SerializeField]
    public int attack;

    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "몬스터 공격력 : " + attack;
    }


}
