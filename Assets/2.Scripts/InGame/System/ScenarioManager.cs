using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    public ExcelParser excelParser;
    private GameManager gameManager;
    public List<Dictionary<string, object>> eventNumDialogue;
    public CharacterSelect characterSelect;
    public Tutorial tutorialManager;

    public TMP_Text talker;
    public TMP_Text content;

    public GameObject conversationGuide;
    public GameObject Conversation;

    public TMP_Text itemTalker;
    public TMP_Text itemContent;
    public Image itemImage;

    public GameObject itemConversation;

    public GameObject tutorial;
    public GameObject KeyGuideUI;
    public GameObject world;
    public GameObject playerSpawnPosDR;
    public GameObject playerSpawnPosKey;
    public GameObject playerSpawnPosOut;
    public GameObject totheKeyLockerPortal;
    public GameObject totheGroundPortal;

    public int conversationIdx;
    public int curConversationNum;
    public int curItemConversationNum;

    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static ScenarioManager instance = null;

    public static ScenarioManager Instance
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
        gameManager = GameManager.Instance;

        FirstScenarioStart();
    }
    
    public void FirstScenarioStart()
    {
        ConversationStart(0);
        conversationGuide.SetActive(true);
    }

    public void FirstScenarioEnd()
    {
        Conversation.SetActive(false);
        conversationGuide.SetActive(false);
        characterSelect.CharacterSelectStart();
    }

    public void TutorialStart()
    {
        tutorialManager.TutorialStart();
    }

    public void TutorialEnd()
    {
        tutorial.SetActive(false);
        KeyGuideUI.SetActive(false);
        world.SetActive(true);
        GameManager.Instance.isTutorial = false;

        //주석변경

        //GameManager.Instance.player.transform.position = playerSpawnPosOut.transform.position;
        //GameManager.Instance.isOnGround = true;

        GameManager.Instance.player.transform.position = playerSpawnPosDR.transform.position;
        GameManager.Instance.isInDRWorldHouse = true;

        Color col = Conversation.transform.GetChild(0).GetComponent<Image>().color;
        col.a = 0.9f;
        Conversation.transform.GetChild(0).GetComponent<Image>().color = col;
        col = Conversation.transform.GetChild(1).GetComponent<Image>().color;
        col.a = 0.9f;
        Conversation.transform.GetChild(1).GetComponent<Image>().color = col;
    }

    /***********************************************************************
    *                               Conversation
    ***********************************************************************/
    #region .
    public void ConversationStart(int eventNum)
    {
        curConversationNum = eventNum;
        conversationIdx = 0;
        Conversation.SetActive(true);
        gameManager.conversationGoing = true;
        GetEventNumberDialogue(eventNum);
        changeText();
    }

    public void ConversationEnd()
    {
        Conversation.SetActive(false);
        gameManager.conversationGoing = false;
        if (gameManager.player!=null)
            gameManager.player.GetComponent<PlayerStatus>().isTalking = false;
    }

    public void GetEventNumberDialogue(int eventNum)
    {
        eventNumDialogue = excelParser.dialogue[eventNum];
    }

    public void changeText()
    {
        if (conversationIdx >= eventNumDialogue.Count)
            return;

        talker.text = eventNumDialogue[conversationIdx]["Talker"].ToString();
        if (talker.text == "나")
            talker.text = GameManager.Instance.playerName;
        content.text = eventNumDialogue[conversationIdx]["Conversation"].ToString();
    }
    #endregion

    /***********************************************************************
    *                               ItemConversation
    ***********************************************************************/
    #region .

    public void ItemConversationStart(int ItemNum)
    {
        curItemConversationNum = ItemNum;
        itemConversation.SetActive(true);
        gameManager.itemconversationGoing = true;
        changeItemText();
    }

    public void ItemConversationEnd()
    {
        itemConversation.SetActive(false);
        gameManager.itemconversationGoing = false;
        if (gameManager.player != null)
            gameManager.player.GetComponent<PlayerStatus>().isTalking = false;
    }

    public void changeItemText()
    {
        itemTalker.text = ItemManager.Instance.itemTotalList[curItemConversationNum].showItemName();
        itemContent.text = ItemManager.Instance.itemTotalList[curItemConversationNum].showItemDescription();
        itemImage.sprite = ItemManager.Instance.itemTotalList[curItemConversationNum].showSprite();
    }
    #endregion

}
