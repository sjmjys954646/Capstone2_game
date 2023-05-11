using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ExcelParser excelParser;
    [SerializeField]
    private int itemNumber;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;

    private void Start()
    {
        excelParser = ExcelParser.Instance;
        itemName = excelParser.itemList[itemNumber][0]["ItemName"].ToString();
        itemDescription = excelParser.itemList[itemNumber][0]["itemDescription"].ToString();
    }
}
