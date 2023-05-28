using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrWorldNpc : Npc
{
    bool firstCoverse = true;

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
            //���� ���
            if(InventoryManager.Instance.InventoryContentNum() == 3)
            {
                PuzzleManager.Instance.PuzzleStart();
            }
            

            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }

        
    }
}
