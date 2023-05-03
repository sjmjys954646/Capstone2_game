using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerMove_Rito_Follow playerMoveInput;

    private ScenarioManager scenarioInput;
    private GameManager gameManager;

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
    }
}
