using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConversation : MonoBehaviour
{
    [SerializeField]
    private bool isInteracting;
    [SerializeField]
    public bool isTalking;
    private void Start()
    {
    }

    private void Update()
    {
        isInteracting = transform.parent.GetComponent<PlayerStatus>().isInteracting;
        isTalking = transform.parent.GetComponent<PlayerStatus>().isTalking;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Npc") && isInteracting && !isTalking)
            other.gameObject.GetComponent<Npc>().Print();
    }
}
