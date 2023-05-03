using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerMove_Rito_Follow playerMoveInput;
    public ScenarioManager scenarioInput;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어가 생성된 이후에 playerMoveInput을 만들어줘야됨
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
