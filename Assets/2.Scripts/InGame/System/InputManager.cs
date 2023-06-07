using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerMove_Rito_Follow playerMoveInput;

    private ScenarioManager scenarioInput;
    private GameManager gameManager;

    //for test
    [SerializeField]
    private GameObject starPos;
    [SerializeField]
    private GameObject guard;
    [SerializeField]
    private GameObject starMaker;
    [SerializeField]
    private GameObject slymeVisitor;

    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static InputManager instance = null;

    public static InputManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        scenarioInput = ScenarioManager.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPlayerExist && !gameManager.isPuzzleGoing)
        {
            if (!gameManager.conversationGoing && !gameManager.itemconversationGoing)
            {
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().CameraViewToggle();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().SetValuesByKeyInput();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().ShowCursorToggle();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().CheckDistanceFromGround();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().Rotate();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().Move();
                gameManager.player.GetComponent<PlayerMove_Rito_Follow>().Jump();
            }

            if (gameManager.isInventoryOpen)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    InventoryManager.Instance.CloseInventory();
                }
            }
            else if (!gameManager.isInventoryOpen)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    InventoryManager.Instance.OpenInventory();
                }
            }

            //testcode
            if(Input.GetKeyDown(KeyCode.F1))
            {
                gameManager.player.transform.position = starPos.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                gameManager.player.transform.position = guard.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                gameManager.player.transform.position = starMaker.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                gameManager.player.transform.position = slymeVisitor.transform.position;
            }
        }

        

        if (gameManager.conversationGoing)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                scenarioInput.conversationIdx++;

                //처음 시나리오에만 예외처리
                if (scenarioInput.curConversationNum == 0 && scenarioInput.conversationIdx >= scenarioInput.eventNumDialogue.Count)
                    scenarioInput.FirstScenarioEnd();

                if (scenarioInput.conversationIdx >= scenarioInput.eventNumDialogue.Count)
                    scenarioInput.ConversationEnd();

                scenarioInput.changeText();
            }
        }

        if (gameManager.itemconversationGoing)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                scenarioInput.ItemConversationEnd();
            }
        }
    }
}
