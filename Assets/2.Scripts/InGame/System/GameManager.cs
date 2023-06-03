using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool conversationGoing = false;
    public bool isPlayerExist = false;
    public bool isTutorial = false;
    public bool isInventoryOpen = false;
    public bool itemconversationGoing = false;
    public bool isPuzzleGoing = false;

    public string playerName;
    public string playerAge;

    public int playerAssetNum;
    public List<GameObject> characters = new List<GameObject>();

    public GameObject player;
    public Camera mainCam;

    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static GameManager instance = null;

    public static GameManager Instance
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

    private void Update()
    {

    }
}
