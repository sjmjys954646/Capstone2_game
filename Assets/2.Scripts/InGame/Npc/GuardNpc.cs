using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardNpc : Npc
{
    bool firstCoverse = true;
    bool puzzleFin = false;
    bool portalCreated = false;
    bool contentCheck = false;
    [SerializeField]
    private GameObject oathSign;


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
            if (!contentCheck && InventoryManager.Instance.InventoryContentNum() >= 10)
            {
                scenarioNum++;
                contentCheck = true;
                player.GetComponent<PlayerStatus>().isTalking = true;
                scenarioManager.ConversationStart(scenarioNum);
                return;
            }

            //���� ����
            if (!puzzleFin && scenarioNum == initscenarioNum + 2)
            {
                PuzzleManager.Instance.PuzzleStart(3);
                puzzleFin = true;
                scenarioNum++;
                return;
            }

            //��Ż ����
            if (scenarioNum == initscenarioNum + 3 && !portalCreated)
            {
                oathSign.SetActive(true);
                portalCreated = true;
                gameObject.GetComponent<ParticleSystem>().Stop();
            }

            if (player == null)
                player = GameManager.Instance.player;

            player.GetComponent<PlayerStatus>().isTalking = true;
            scenarioManager.ConversationStart(scenarioNum);
        }


    }
}
