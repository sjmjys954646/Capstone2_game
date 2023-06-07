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

    public bool isInDRWorldHouse = false;
    public bool isInKeyLockerRoom = false;
    public bool isOnGround = false;

    [SerializeField]
    private float time;

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
        time += Time.deltaTime;
        if (time > 2f)
        {
            time = 0;
            playerBugFix();
        }
    }


    public void playerBugFix()
    {
        if (isPuzzleGoing)
            return;

        if (!isPlayerExist)
            return;

        if(isInDRWorldHouse )
        {
            if(player.transform.position.y < 40f)
            {
                player.transform.position = ScenarioManager.Instance.playerSpawnPosDR.transform.position;
            }
        }
        else if(isOnGround)
        {
            if (player.transform.position.y < -30f)
            {
                player.transform.position = ScenarioManager.Instance.playerSpawnPosOut.transform.position;
            }
        }
        else if (isInKeyLockerRoom)
        {
            if (player.transform.position.y < 40f)
            {
                player.transform.position = ScenarioManager.Instance.playerSpawnPosKey.transform.position;
            }
        }
    }
}
