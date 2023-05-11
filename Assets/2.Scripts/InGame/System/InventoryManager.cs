using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI;
    [SerializeField]
    private List<GameObject> inventoryContent = new List<GameObject>();
    [SerializeField]
    private GameObject[,] inventoryUIIndiv = new GameObject[4, 5];


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
        SetInventoryUI();
    }

    public void OpenInventory()
    {
        GameManager.Instance.isInventoryOpen = true;
        inventoryUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInventory()
    {
        GameManager.Instance.isInventoryOpen = false;
        inventoryUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetInventoryUI()
    {
        for(int j = 0;j < 5 ;j++)
        {
            for (int i = 0; i < 4; i++)
            {
                inventoryUIIndiv[i, j] = inventoryUI.transform.GetChild(4 * i + j).gameObject;
            }
        }
    }

    public void AddInventory()
    {

    }
}
