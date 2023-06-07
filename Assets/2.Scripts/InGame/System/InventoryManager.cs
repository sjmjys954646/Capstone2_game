using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI;
    [SerializeField]
    private List<Item> inventoryContent = new List<Item>();
    [SerializeField]
    private GameObject[,] inventoryUIIndiv = new GameObject[4,5];

    private int curInvenNum = 0;


    /***********************************************************************
   *                               SingleTon
   ***********************************************************************/
    #region .
    private static InventoryManager instance = null;

    public static InventoryManager Instance
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
        SetInventoryUI();
    }

    public List<Item> InventoryShow()
    {
        return inventoryContent;
    }

    public int InventoryContentNum()
    {
        return inventoryContent.Count;
    }

    public void OpenInventory()
    {
        GameManager.Instance.isInventoryOpen = true;
        inventoryUI.SetActive(true);
        Time.timeScale = 0;

        if(curInvenNum != inventoryContent.Count)
        {
            refreshInventory();
        }
    }

    public void CloseInventory()
    {
        GameManager.Instance.isInventoryOpen = false;
        inventoryUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetInventoryUI()
    {
        for(int i = 0;i < 4 ;i++)
        {
            for (int j = 0; j < 5; j++)
            {
                inventoryUIIndiv[i, j] = inventoryUI.transform.GetChild(5 * i + j).gameObject;
            }
        }
    }

    private void refreshInventory()
    {
        int itemNum = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (itemNum == inventoryContent.Count)
                    break;

                inventoryUIIndiv[i, j].GetComponent<Image>().sprite = inventoryContent[itemNum].showSprite();

                itemNum++;
            }

            if (itemNum == inventoryContent.Count)
                break;
        }
        curInvenNum = inventoryContent.Count;
    }

    public void AddInventory(Item item)
    {
        inventoryContent.Add(item);
    }
}
