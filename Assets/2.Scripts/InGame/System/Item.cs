using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager;
    [SerializeField]
    private int itemNumber;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private Sprite sprite;

    public void ItemSetter(int a, string b, string c, Sprite d)
    {
        itemNumber = a;
        itemName = b;
        itemDescription = c;
        sprite = d;
    }

    private void Start()
    {
        itemManager = ItemManager.Instance;
        itemName = itemManager.itemTotalList[itemNumber].itemName;
        itemDescription = itemManager.itemTotalList[itemNumber].itemDescription;
        sprite = itemManager.itemTotalList[itemNumber].sprite;
    }

    public int showItemNumber()
    {
        return itemNumber;
    }

    public string showItemName()
    {
        return itemName;
    }

    public string showItemDescription()
    {
        return itemDescription;
    }

    public Sprite showSprite()
    {
        return sprite;
    }

    public void PrintAndGet()
    {
        InventoryManager.Instance.AddInventory(gameObject.GetComponent<Item>());
        ScenarioManager.Instance.ItemConversationStart(itemNumber);
        gameObject.SetActive(false);
    }
}
