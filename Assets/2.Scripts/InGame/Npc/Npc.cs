using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject player;
    public ScenarioManager scenarioManager;

    [SerializeField]
    private int scenarioNum;

    private void Start()
    {
        scenarioManager = ScenarioManager.Instance;
    }

    public void Print()
    {
        if (player == null)
            player = GameManager.Instance.player;

        player.GetComponent<PlayerStatus>().isTalking = true;
        scenarioManager.ConversationStart(scenarioNum);
    }

}
