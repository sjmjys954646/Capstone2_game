using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private GameObject drWorldHouse;
    [SerializeField]
    private GameObject keyLockerRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            keyLockerRoom.SetActive(true);
            GameManager.Instance.player.transform.position = spawnPos.transform.position;
            drWorldHouse.SetActive(false);
            GameManager.Instance.isInDRWorldHouse = false;
            GameManager.Instance.isInKeyLockerRoom = true;
            StartCoroutine(startConverse());
        }
    }

    IEnumerator startConverse()
    {
        yield return new WaitForSeconds(0.2f);
        ScenarioManager.Instance.ConversationStart(4);
    }
}
