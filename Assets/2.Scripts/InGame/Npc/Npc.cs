using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject player;
    public ScenarioManager scenarioManager;
    [SerializeField]
    protected int initscenarioNum;
    [SerializeField]
    protected int scenarioNum;

    private void Start()
    {
        scenarioManager = ScenarioManager.Instance;
        initscenarioNum = scenarioNum;
    }

    public virtual void Print()
    {
        if (player == null)
            player = GameManager.Instance.player;

        player.GetComponent<PlayerStatus>().isTalking = true;
        scenarioManager.ConversationStart(scenarioNum);
    }

}
