using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private ExcelParser excelParser;
    [SerializeField]
    private Sprite[] sprites;

    public List<Item> itemTotalList;

    /***********************************************************************
  *                               SingleTon
  ***********************************************************************/
    #region .
    private static ItemManager instance = null;

    public static ItemManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        excelParser = ExcelParser.Instance;
        itemTotalList = new List<Item>();
        fillItems();
    }

    private void fillItems()
    {
        sprites = Resources.LoadAll<Sprite>("item");

        for (int itemNumber = 0; itemNumber < excelParser.itemList.Count; itemNumber++)
        {
            string itemName = excelParser.itemList[itemNumber][0]["ItemName"].ToString();
            string itemDescription = excelParser.itemList[itemNumber][0]["ItemDescription"].ToString();


            Item item = new Item();
            item.ItemSetter(itemNumber, itemName, itemDescription, sprites[itemNumber]);

            itemTotalList.Add(item);
        }
           
    }


}
