using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLockerNpc : Npc
{
    bool firstCoverse = true;
    bool puzzleFin = false;
    bool portalCreated = false;
    bool contentCheck = false;

    public override void Print()
    {
        if (firstCoverse)
        {
            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);

            scenarioNum++;
            firstCoverse = false;
        }
        else
        {
            //����Ȯ��
            if (!contentCheck && InventoryManager.Instance.InventoryContentNum() == 7)
            {
                scenarioNum++;
                contentCheck = true;
                player.GetComponent<PlayerStatus>().isTalking = true;
                scenarioManager.ConversationStart(scenarioNum);
                return;
            }

            //���� ����
            if(!puzzleFin && scenarioNum == initscenarioNum + 2)
            {
                PuzzleManager.Instance.PuzzleStart(1);
                puzzleFin = true;
                scenarioNum++;
                return;
            }

            //��Ż ����
            if (scenarioNum == initscenarioNum + 3 && !portalCreated)
            {
                ScenarioManager.Instance.totheGroundPortal.SetActive(true);
                portalCreated = true;
            }

            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }


    }
}
