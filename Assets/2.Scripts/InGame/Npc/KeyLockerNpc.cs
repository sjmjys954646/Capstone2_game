using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLockerNpc : Npc
{
    bool firstCoverse = true;
    bool puzzleFin = false;
    bool portalCreated = false;

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
            //문제 출력
            if (!puzzleFin && InventoryManager.Instance.InventoryContentNum() == 7)
            {
                PuzzleManager.Instance.PuzzleStart(1);
                puzzleFin = true;
                scenarioNum++;
                return;
            }

            if (player == null)
                player = GameManager.Instance.player;

            //if (scenarioNum == 3 && !portalCreated)
            //{
            //    ScenarioManager.Instance.totheGroundPortal.SetActive(true);
            //    portalCreated = true;
            //}

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }


    }
}
