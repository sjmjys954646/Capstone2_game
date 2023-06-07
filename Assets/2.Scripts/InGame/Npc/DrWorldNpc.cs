using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrWorldNpc : Npc
{
    bool firstCoverse = true;
    bool puzzleFin = false;
    bool portalCreated = false;
    bool contentCheck = false;

    public override void Print()
    {
        if(firstCoverse)
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
            //내용확인
            if (!contentCheck && InventoryManager.Instance.InventoryContentNum() == 3)
            {
                scenarioNum++;
                contentCheck = true;
                player.GetComponent<PlayerStatus>().isTalking = true;
                scenarioManager.ConversationStart(scenarioNum);
                return;
            }

            //문제 출력
            if (!puzzleFin && scenarioNum == initscenarioNum + 2)
            {
                PuzzleManager.Instance.PuzzleStart(0);
                puzzleFin = true;
                scenarioNum++;
                return;
            }

            if (scenarioNum == initscenarioNum + 3 && !portalCreated)
            {
                ScenarioManager.Instance.totheKeyLockerPortal.SetActive(true);
                portalCreated = true;
                return;
            }

            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }

        
    }
}
