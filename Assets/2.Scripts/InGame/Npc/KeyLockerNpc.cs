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
            if (InventoryManager.Instance.InventoryContentNum() == 4)
            {
                scenarioNum++;
            }

            if(!puzzleFin && scenarioNum == 8)
            {
                PuzzleManager.Instance.PuzzleStart(1);
                puzzleFin = true;
                return;
            }

            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }


    }
}
