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
        // �÷��̾ ������ ���Ŀ� playerMoveInput�� �������ߵ�
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.conversationGoing)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                scenarioInput.conversationIdx++;

                //ó�� �ó��������� ����ó��
                if (scenarioInput.curConversationNum == 0 && scenarioInput.conversationIdx >= scenarioInput.eventNumDialogue.Count)
                    scenarioInput.FirstScenarioEnd();

                if (scenarioInput.conversationIdx >= scenarioInput.eventNumDialogue.Count)
                    scenarioInput.ConversationEnd();

                scenarioInput.changeText();
            }
        }
    }
}
